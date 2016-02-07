﻿using Kong.OnlineStoreAPI.Model;
using Microsoft.Practices.ServiceLocation;
using System.Web.Http;

namespace Kong.OnlineStoreAPI.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private IUserMgr userMgr;

        public UserController()
        {
            userMgr = ServiceLocator.Current.GetInstance<IUserMgr>();
        }

        [HttpGet]
        [Route("route/user/get")]
        public IHttpActionResult Retrieve()
        {
            var user = new User();
            user.Email = "mkong@dealertrac.co";
            user.Password = "11111";
            return Ok(user);
        }

        [HttpPost]
        [Route("route/user/new")]
        public IHttpActionResult Register([FromBody]User user)
        {
            return Ok(userMgr.Add(user));
        }


        [HttpPost]
        [Route("route/user/login")]
        public IHttpActionResult Login([FromBody]User user)
        {
            return Ok(userMgr.Login(user));
        }

        [HttpPost]
        [Route("route/user/activate")]
        public IHttpActionResult Activate([FromBody]User user)
        {
            return Ok(userMgr.Activate(user));
        }

        [HttpPost]
        [Route("route/user/recoverPwd")]
        public IHttpActionResult RecoverPassword([FromBody]User user)
        {
            return Ok(userMgr.RecoverPassword(user));
        }
    }
}
