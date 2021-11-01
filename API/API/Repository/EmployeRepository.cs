using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeRepository : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeRepository(MyContext context)
        {
            this.context = context;

        }
        public int Delete(string NIK)
        {

            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
        }

        public int GetCheck()
        {
            var value = Get();
            if (value.Count() > 0)
            {
                return 1;
            }
            return 0;
        }

        public int Insert(Employee employee)
        {
            var nik = employee.NIK;
            var email = employee.Email;
            var phone = employee.Phone;
            var value = Get();

            if (nik == "")
            {
                return 1;
            }else if (nik == null)
            {
                return 2;
            }
            else
            {
                foreach (var data in value)
                {
                    if (data.NIK == nik)
                    {
                        return 3;
                    }else if (data.Email == email)
                    {
                        return 4;
                    }else if (data.Phone == phone)
                    {
                        return 6;
                    }
                }
                context.Employees.Add(employee);
                var result = context.SaveChanges();
                return result;
            }
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
    }
}
