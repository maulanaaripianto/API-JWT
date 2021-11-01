using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeRepository employeesRepository;
    
        public EmployeeController(EmployeRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            var validation = employeesRepository.Insert(employee);
            if (validation == 1)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "0", message = "NiK Tidak Boleh Kosong " });
            }
            else if (validation == 2)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "0", message = "NiK Tidak Boleh Null " });
            }
            else if (validation == 3)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = $"0", message = $"NIK {employee.NIK} sudah ada" });
            }
            else if (validation == 4)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = $"0", message = $"Email {employee.Email} sudah ada" });
            }
            else if (validation == 5)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = $"0", message = $"Number Phone {employee.Phone} sudah ada" });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = "1", message = "Berhasil Memasukkan Data Baru " });
            }
        }

        [HttpGet]
        public ActionResult Get()
        {
            var value = employeesRepository.GetCheck();
            if (value == 1)
            {
                var x = employeesRepository.Get();
                return Ok(new { status = HttpStatusCode.OK, result = x, message = "Data Employee" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = "", message = "Data tidak ditemukan " });
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            
            var result = employeesRepository.Get(NIK);
            if (result == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = $"{result}", message = "Data Tidak Ditemukan " });
            }
            else
            {
                return Ok(new { status = HttpStatusCode.OK, result = $"{result}", message = "Data Ditemukan" });
            }
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            try
            {
                employeesRepository.Delete(NIK);
                return Ok(new { status = HttpStatusCode.OK, result = $"0", message = $"Berhasil Menghapus Data NIK {NIK}" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "1", message = $"Data Nik {NIK} Tidak Ditemukan" });
            }
        }

        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            try
            {
                var result = employeesRepository.Update(employee);
                return Ok(new { status = HttpStatusCode.OK, result = $"{result}", message = $"Data NIK : {employee.NIK} berhasil di update"});
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = $"Data Nik {employee.NIK} Tidak Ditemukan" });
            } 
        }

    }
}
