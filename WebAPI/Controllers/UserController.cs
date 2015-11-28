using Kong.OnlineStoreAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Kong.OnlineStoreAPI.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        // GET route/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET route/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var user = new User();
            user.Email = "mkong@dealertrac.co";
            user.Password = "11111";
            return Ok(user);
        }

        // POST route/<controller>
        public IHttpActionResult Post([FromBody]User user)
        {

            return Ok(0);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]User user)
        {
            return Ok(0);

        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            return Ok(0);

        }
    }
}
