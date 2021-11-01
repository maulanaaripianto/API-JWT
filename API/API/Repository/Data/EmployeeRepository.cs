using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var nik = registerVM.NIK;
            var phone = registerVM.phoneNumber;
            var email = registerVM.email;
            var value = myContext.Employees.ToList();
            foreach (var isResult in value)
            {
                if (isResult.NIK == nik)
                {
                    return 1;
                }
                else if (isResult.Email == email)
                {
                    return 2;
                }
                else if (isResult.Phone == phone)
                {
                    return 3;
                }
            }

            Employee employee = new Employee();
            employee.NIK = registerVM.NIK;
            employee.FirstName = registerVM.firstName;
            employee.LastName = registerVM.lastName;
            employee.Phone = registerVM.phoneNumber;
            employee.BirthDate = registerVM.birthDate;
            employee.Salary = registerVM.salary;
            employee.Email = registerVM.email;
            employee.Gender = registerVM.Gender;
            myContext.Employees.Add(employee);
            myContext.SaveChanges();

            var x = GetRandomSalt();
            Account account = new Account();
            account.NIK = registerVM.NIK;
            account.Password = BCrypt.Net.BCrypt.HashPassword(registerVM.password, x);
            myContext.Account.Add(account);
            myContext.SaveChanges();

            Education edu = new Education();
            edu.Degree = registerVM.degree;
            edu.Gpa = registerVM.gpa;
            edu.UniversityId = registerVM.university_Id;
            myContext.Education.Add(edu);
            myContext.SaveChanges();

            Profiling profiling = new Profiling();
            profiling.NIK = registerVM.NIK;
            profiling.EducationId = edu.Id;
            myContext.Profilings.Add(profiling);
            myContext.SaveChanges();


            AccountRoles accountRoles = new AccountRoles();
            accountRoles.AccountNIK = registerVM.NIK;
            accountRoles.RolesId = registerVM.roles_id;
            myContext.AccountRoles.Add(accountRoles);
            myContext.SaveChanges();

            return 0;
        }

        public IEnumerable GetProfile()
        {
            var result = from emp in myContext.Employees
                         join acc in myContext.Account on emp.NIK equals acc.NIK
                         join prof in myContext.Profilings on acc.NIK equals prof.NIK
                         join edu in myContext.Education on prof.EducationId equals edu.Id
                         join univ in myContext.Universities on edu.UniversityId equals univ.Id
                         select new
                         {
                             NIK = emp.NIK,
                             FullName = emp.FirstName + " " + emp.LastName,
                             PhoneNumber = emp.Phone,
                             BirthDate = emp.BirthDate,
                             Salary = emp.Salary,
                             Email = emp.Email,
                             Password = acc.Password,
                             Degree = edu.Degree,
                             Gpa = edu.Gpa,
                             UniversityName = univ.Name
                         };
            return result.ToList();
        }

        public IEnumerable GetProfile(String key)
        {
            var rolesId = (from acc in myContext.Account
                       join accroles in myContext.AccountRoles on acc.NIK equals accroles.AccountNIK
                       where accroles.AccountNIK == key
                       select accroles.Account_Id).Single();

            var nik = (from acc in myContext.Account
                           join accroles in myContext.AccountRoles on acc.NIK equals accroles.AccountNIK
                           where accroles.AccountNIK == key
                           select accroles.AccountNIK).Single();

            var value = myContext.AccountRoles.ToList();
            foreach (var isResult in value)
            {
                if (key == nik)
                {
                    if (rolesId == isResult.Account_Id)
                    {
                        var result = from emp in myContext.Employees
                                     join acc in myContext.Account on emp.NIK equals acc.NIK
                                     join prof in myContext.Profilings on acc.NIK equals prof.NIK
                                     join edu in myContext.Education on prof.EducationId equals edu.Id
                                     join univ in myContext.Universities on edu.UniversityId equals univ.Id
                                     where emp.NIK == key
                                     select new
                                     {
                                         NIK = emp.NIK,
                                         FullName = emp.FirstName + " " + emp.LastName,
                                         PhoneNumber = emp.Phone,
                                         BirthDate = emp.BirthDate,
                                         Salary = emp.Salary,
                                         Email = emp.Email,
                                         Password = acc.Password,
                                         Degree = edu.Degree,
                                         Gpa = edu.Gpa,
                                         UniversityName = univ.Name
                                     };
                        return result.ToList();
                    }
                }
            }
            return null;
        }

        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public int Login(LoginVM loginVM)
        {
            var email = loginVM.email;
            var password = loginVM.password;
            var value = myContext.Employees.ToList();
            string dataNIK;
            string dataPassword;
            int roleId;

            try
            {
                dataNIK = (from emp in myContext.Employees
                           where emp.Email == email
                           select emp.NIK).Single();
                dataPassword = (from emp in myContext.Employees
                                join acc in myContext.Account on emp.NIK equals acc.NIK
                                where emp.Email == email
                                select acc.Password).Single();

                roleId = (from acc in myContext.Account
                          join accroles in myContext.AccountRoles on acc.NIK equals accroles.AccountNIK
                          join role in myContext.Roles on accroles.RolesId equals role.Id
                          where (acc.NIK == dataNIK)
                          select accroles.RolesId).Single();
            }
            catch (InvalidOperationException)
            {
                return 0;

            }

            var pass = BCrypt.Net.BCrypt.Verify(password, dataPassword);

            foreach (var isResult in value)
            {
                if (isResult.Email == dataNIK && pass == true)
                {
                    if (roleId == 1)
                    {
                        return 1;
                    }
                    else if (roleId == 1)
                    {
                        return 2;
                    }
                    else if (roleId == 1 && roleId == 2)
                    {
                        return 3;
                    }
                }
                else if (isResult.Email == dataNIK && password == null)
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }
            return 3;
        }

        public int LoginData(LoginVM loginVM)
        {
            var email = loginVM.email;
            var password = loginVM.password;

            var dataNIK = (from emp in myContext.Employees
                       where emp.Email == email
                       select emp.NIK).Single();

            var dataPassword = (from emp in myContext.Employees
                            join acc in myContext.Account on emp.NIK equals acc.NIK
                            where emp.Email == email
                            select acc.Password).Single();
            return 0;
        }

        public string[] GetUserData(LoginVM loginVM)
        {
            var nik = (from emp in myContext.Employees
                       where emp.Email == loginVM.email
                       select emp.NIK).FirstOrDefault();
            var roles = myContext.AccountRoles.Where(r => r.AccountNIK == nik).ToList();
            List<string> value = new List<string>();

            foreach(var result in roles)
            {
                value.Add(myContext.Roles.Where(r => r.Id == result.RolesId).First().Role_Name);
            }
            return value.ToArray();
        }

        public int SignManager(string key)
        {
            AccountRoles accountRoles = new AccountRoles();
            accountRoles.AccountNIK = key;
            accountRoles.RolesId = 2;
            myContext.Add(accountRoles);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
