using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace UserAdminAuthentication.Model
{
    public class MockUserAdminRepository : IUserAdminRepository
    {
        private readonly IConfiguration _configuration;
        private readonly UserAdminDbContext context;
        public static UserAdmin user = new UserAdmin();
        public MockUserAdminRepository(UserAdminDbContext Context, IConfiguration configuration)
        {
            this.context = Context;
            this._configuration = configuration;
        }


        #region Registration
        public bool Register(UserAdmin input,string password)
        {
            var hasValue = context.UserAdmins.Where(s => s.Email == input.Email).FirstOrDefault();
            if (hasValue != null)
            {
                return false;
            }
            CreateHashPassword(password, out byte[] hashPassword, out byte[] saltPassword);
            input.HashPassword = hashPassword;
            input.SaltPassword = saltPassword;
            context.Add(input);
            return context.SaveChanges() == 1 ? true : false;

        }

        private void CreateHashPassword(string password, out byte[] hashPassword, out byte[] saltPassword)
        {
            using (var hmac = new HMACSHA512())
            {
                saltPassword = hmac.Key;
                hashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            };
        }
        #endregion


        #region login

        public UserAdmin login(string email, string password)
        {
            var userRow = context.UserAdmins.Where(s => s.Email == email).FirstOrDefault();
            if (userRow != null)
            {
                var isValidPass = PasswordCheck(password, userRow.HashPassword, userRow.SaltPassword);
                 
                return userRow.Email == email && isValidPass ? userRow : null;
            }
           
            return null;

            //string token = CreateToken(user);

            //var RefreshToken = GenerateRefreshToken();
            //SetReferenceToken(RefreshToken);


        }

        private bool PasswordCheck(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }

       
        public string CreateToken(UserAdmin user)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name,user.Email)
                
            };
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                claims.Add(new Claim("Role", "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("Role", "User"));
            }

            var keys = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value));
            var cred = new SigningCredentials(keys, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        public RefreshToken GenerateRefreshToken()
        {
            Random rnd = new Random();
            var buffer = new byte[sizeof(Int64)];
            rnd.NextBytes(buffer);
            
            var refeshToken = new RefreshToken
            {
            
                Token = Convert.ToBase64String(buffer),
                Expires = DateTime.Now.AddDays(1),
                Created = DateTime.Now
            };
            return refeshToken;

        }

        //private void SetReferenceToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expires

        //    };
        //    Response.Cookies.Append("RefreshToken", newRefreshToken.Token, cookieOptions);
        //    user.RefreshToken = newRefreshToken.Token;
        //    user.TokenCreated = newRefreshToken.Created;
        //    user.TokenExpired = newRefreshToken.Expires;


        //}


        #endregion

    }
}
