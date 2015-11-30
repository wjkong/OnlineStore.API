using Kong.OnlineStoreAPI.Logic;
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
        protected UserMgr userMgr = new UserMgr();

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
            return Ok(userMgr.Add(user));
        }

        // POST route/<controller>/action
        public IHttpActionResult Post(string action, [FromBody]User user)
        {
            return Ok(userMgr.Login(user));
        }

        // PUT route/<controller>
        public IHttpActionResult Put([FromBody]User user)
        {
            return Ok(userMgr.Modify(user));
        }

        // PUT route/<controller>/action
        public IHttpActionResult Put(string action, [FromBody]User user)
        {
            return Ok(userMgr.Modify(action, user));
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            return Ok(0);

        }
    }
}
