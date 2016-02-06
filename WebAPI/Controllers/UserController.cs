using Kong.OnlineStoreAPI.Model;
using Microsoft.Practices.ServiceLocation;
using NLog;
using System.Web.Http;

namespace Kong.OnlineStoreAPI.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        //private IUserMgr userMgr;
        private IUserMgr userMgr;
        private static Logger logger = LogManager.GetCurrentClassLogger();

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
            //logger.Info("Register a new user: " + user.Email);

            return Ok(userMgr.Add(user));
        }

        [HttpPost]
        [Route("route/user/update")]
        public IHttpActionResult Update([FromBody]User user)
        {
            //logger.Info("Register a new user: " + user.Email);

            return Ok(userMgr.Modify(user));
        }

        [HttpPost]
        [Route("route/user/login")]
        public IHttpActionResult Login([FromBody]User user)
        {
            //logger.Info("Register a new user: " + user.Email);

            return Ok(userMgr.Login(user));
        }
    }
}
