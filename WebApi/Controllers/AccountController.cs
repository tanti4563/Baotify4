using Service.Helper;
using Service.Interfaces;
using Service.Models;
using System.Web.Http;

namespace Service.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody] LoginModel data)
        {
            new LoggingHelper().WriteLog("User login: " + data.Username);
            var user = _userService.Login(data.Username, data.Password);
            if (user == null)
            {
                return Ok(new ReturnObjectModel { Status = false, Code = "01", Message="Invalid username or password", Data = null }) ;
            }
            user.Password = "*********";
            return Ok(new ReturnObjectModel { Status = true, Code = "00", Message = "Login success", Data = user });
        }

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register([FromBody] RegisterModel data)
        {
            if (data.Password != data.Repassword) {
                return Ok(new ReturnObjectModel { Status = false, Code = "02", Message = "Password and confirm password not match", Data = null });
            }

            if (data.Password.Length < 6)
            {
                return Ok(new ReturnObjectModel { Status = false, Code = "03", Message = "Password too weak", Data = null });
            }
            if (_userService.Register(data))
            {
                return Ok(new ReturnObjectModel { Status = true, Code = "00", Message = "Register success", Data = null });
            }
            else
            {
                return Ok(new ReturnObjectModel { Status = false, Code = "01", Message = "Register failed ", Data = null });
            }
        }


        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(string key)
        {
            return Ok(new ReturnObjectModel { 
                Status = true, 
                Code = "00", 
                Message = "Register success", 
                Data = _userService.GetListAccounts(key) 
            });
        }
    }

}