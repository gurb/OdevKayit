using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OdevKayit.Models;

namespace OdevKayit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgrenciController : ControllerBase
    {
        private odevlerContext odevlerContext;

        public OgrenciController(odevlerContext odevlerContext)
        {
            this.odevlerContext = odevlerContext;
        }

        // GET: api/<OgrenciController>
        [HttpGet]
        public IEnumerable<Ogrenci> Get()
        {
            return odevlerContext.Ogrenci;
        }

        // GET api/<OgrenciController>/5
        [HttpGet("{id}")]
        public Ogrenci Get(int id)
        {
            return odevlerContext.Ogrenci.FirstOrDefault(o => o.Id == id);
        }

        // sadece o ödeve atanmış öğrencileri listele
        // GET api/<OgrenciController>/listele/5
        [HttpGet("listele/{odevid}")]
        public IEnumerable<Ogrenci> OgrenciList(int odevid)
        {
            return odevlerContext.Ogrenci.Where(o => o.OdevId == odevid);
        }

        // POST api/<OgrenciController>
        [HttpPost]
        public void Post([FromBody] Ogrenci value)
        {
            odevlerContext.Ogrenci.Add(value);
            odevlerContext.SaveChanges();
        }

        // PUT api/<OgrenciController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Ogrenci value)
        {
            Ogrenci ogrenci = odevlerContext.Ogrenci.FirstOrDefault(o => o.Id == id);
            if (ogrenci != null)
            {
                odevlerContext.Entry<Ogrenci>(ogrenci).CurrentValues.SetValues(value);
                odevlerContext.SaveChanges();
            }
        }

        // DELETE api/<OgrenciController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Ogrenci ogrenci = odevlerContext.Ogrenci.FirstOrDefault(o => o.Id == id);
            if (ogrenci != null)
            {
                odevlerContext.Ogrenci.Remove(ogrenci);
                odevlerContext.SaveChanges();
            }
        }
    }
}
