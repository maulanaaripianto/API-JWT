using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                var result = repository.Insert(entity);
                return Ok(new { status = HttpStatusCode.OK, result, message = "Berhasil Memasukkan Data Baru " });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = $"Data sudah ada sebelumnya/Data tidak ditemukan" });
            }
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            try
            {
                var result = repository.Get();
                return Ok(new { status = HttpStatusCode.OK, result, message = "Data Ditemukan" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = $"Data tidak ditemukan" });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            try
            {
                repository.Delete(key);
                return Ok(new { status = HttpStatusCode.OK, result = $"1", message = $"Berhasil Menghapus Data" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "0", message = $"Data  Tidak Ditemukan" });
            }
        }

        [HttpPut]
        public ActionResult Update(Entity entity, Key key)
        {
            try
            {
                var result = repository.Update(entity, key);
                return Ok(new { status = HttpStatusCode.OK, result = $"{result}", message = $"Data berhasil di update" });
            }
            catch (Exception)
            {
                return Ok(new { status = HttpStatusCode.InternalServerError, result = "", message = $"Data Tidak Ditemukan" });
            }
        }
    }
}
