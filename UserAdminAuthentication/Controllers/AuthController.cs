using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UserAdminAuthentication.Model;

namespace UserAdminAuthentication.Controllers
{
    
    [Route("api/[controller]")] 
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAdminRepository userAdmin;
        public static UserAdmin user = new UserAdmin();
        //private readonly UserAdmin  input;
        public AuthController(IUserAdminRepository _userAdmin)
        {
            userAdmin = _userAdmin;
        }

        [HttpGet("Get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Cust1", "Cust2" };
        }

        #region Registration
        [Route("api/auth/register")]
        [HttpPost("Register")]
        public async Task<ActionResult<String>> Register(string FirstName,string LastName,string Email,string Password,bool IsAdmin,string Gender,int PhoneNumber)
        {
            var input = new UserAdmin();
            input.FirstName = FirstName;input.LastName = LastName;input.Email = Email;input.IsAdmin = IsAdmin;input.Gender = Gender;input.PhoneNumber = PhoneNumber;
            return userAdmin.Register(input,Password)?  Ok("Registration Successfill"):  Ok("Registration Failed");            
        }

        #endregion

        #region login

        [Route("api/auth/login")]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string email, string password)
        {
            var ret_loginVal = userAdmin.login(email, password);

            if (ret_loginVal!=null) 
            {
                
                string token = userAdmin.CreateToken(ret_loginVal);
                //var RefreshToken = userAdmin.GenerateRefreshToken();
                //SetReferenceToken(RefreshToken, token);
                return Ok(new {tokenVal = token,user = ret_loginVal});
            }
            return Ok("invalid User/password");
            
        }

        private void SetReferenceToken(RefreshToken newRefreshToken,string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires

            };
            Response.Cookies.Append("RefreshToken", token, cookieOptions);
            //user.RefreshToken = newRefreshToken.Token;
            //user.TokenCreated = newRefreshToken.Created;
            //user.TokenExpired = newRefreshToken.Expires;


        }

        #endregion

    }
}
