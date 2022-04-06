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
    public class OdevController : ControllerBase
    {

        private odevlerContext odevlerContext;

        public OdevController(odevlerContext odevlerContext)
        {
            this.odevlerContext = odevlerContext;

        }

        //GET: api/<OdevController>
        [HttpGet]
        public IEnumerable<Odev> Get()
        {
            return odevlerContext.Odev;
        }

        //GET api/<OdevController>/5
        [HttpGet("{id}")]
        public Odev Get(int id)
        {
            return odevlerContext.Odev.FirstOrDefault(o => o.Id == id);
        }

        //POST api/<OdevController>
        [HttpPost]
        public void Post([FromBody] Odev value)
        {
            odevlerContext.Odev.Add(value);
            odevlerContext.SaveChanges();
        }

        //PUT api/<OdevController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Odev value)
        {
            Odev odev = odevlerContext.Odev.FirstOrDefault(o => o.Id == id);
            if (odev != null)
            {
                odevlerContext.Entry<Odev>(odev).CurrentValues.SetValues(value);
                odevlerContext.SaveChanges();
            }
        }

        //DELETE api/<OdevController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Odev odev = odevlerContext.Odev.FirstOrDefault(o => o.Id == id);
            if (odev != null)
            {
                odevlerContext.Odev.Remove(odev);
                odevlerContext.SaveChanges();
            }
        }
    }
}
