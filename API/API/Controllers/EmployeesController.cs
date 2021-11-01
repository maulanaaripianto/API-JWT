using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._configuration = configuration;
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            var validation = employeeRepository.Register(registerVM);
            if (validation == 1)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, message = "NIK yang anda masukan sudah ada" });
            }
            else if (validation == 2)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, message = "Email yang anda masukan sudah ada" });
            }
            else if (validation == 3)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, message = "Nomor Handphone yang anda masukan sudah ada" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data Berhasil Dimasukkan" });
            }
        }

        [Authorize(Roles = "Director, Manager, Employee")]
        [Route("Profile")]
        [HttpGet]
        public ActionResult GetProfile()
        {
            try
            {
                var result = employeeRepository.GetProfile();
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Employee" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, message = "Data tidak ditemukan" });
            }
        }

        [Authorize]
        [Route("Profile/{Key}")]
        [HttpGet]
        public ActionResult GetProfile(String key)
        {
            try
            {
                var result = employeeRepository.GetProfile(key);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Employee" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, message = "Data tidak ditemukan" });
            }
        }

        [Route("Login")]
        [HttpGet]
        public ActionResult Login(LoginVM loginVM)
        {
            var validation = employeeRepository.Login(loginVM);

            try
            {
                if (validation == 1)
                {
                    return Ok(new { status = HttpStatusCode.OK, message = "Login sebagai Employe" });
                }
                else if (validation == 2)
                {
                    return Ok(new { status = HttpStatusCode.OK, message = "Login sebagai Manager" });
                }
                else if (validation == 3)
                {
                    return Ok(new { status = HttpStatusCode.OK, message = "Login sebagai Employee dan Manager" });
                }
                else if (validation == 4)
                {
                    return Ok(new { status = HttpStatusCode.OK, message = "Password tidak boleh Null" });
                }
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, message = "Password dan Email Salah" });
            }
            return Ok(new { status = HttpStatusCode.InternalServerError, message = "Password dan Email Salah" });
        }

        [Route("LoginDataVM")]
        [HttpGet]
        public ActionResult LoginData(LoginVM loginVM)
        {
            var getUserData = employeeRepository.GetUserData(loginVM);

            var data = new LoginDataVM()
            {
                Email = loginVM.email,
                RoleName = getUserData
            };

            var claims = new List<Claim>
            {
                new Claim("Email", data.Email)
            };

            foreach (var item in data.RoleName)
            {
                claims.Add(new Claim("roles", item.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var sigIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: sigIn);
            var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
            claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
            return Ok(new { status = HttpStatusCode.OK, idtoken, message = "Login Sukses!!" });
        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT berhasil");
        }

        [Authorize (Roles = "Director")]
        [Route("SignManager/{Key}")]
        [HttpPost]
        public ActionResult SignManager(string key)
        {
            var result = employeeRepository.SignManager(key);
            try
            {
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data berhasil insert" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.OK, result= "", message = "Data gagal Insert" });
            }
        }
    }
}
