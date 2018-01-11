using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthentication.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]/[Action]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [Authorize]
        [HttpGet]
        public JsonResult Get()
        {
            return Json(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [Authorize]//(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return User.Identity.Name;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //[HttpPost]
        //public string SynDB([FromBody]Wishlist _wishList)
        //{
        //    ApplicationDbContext applicationDbContext = new ApplicationDbContext();
        //    //applicationDbContext.Add
        //}
    }
}
