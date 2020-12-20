using FluentValidation;
using FluentValidation.Attributes;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace Preveld.Controllers.api
{
    [Validator(typeof(UserValidator))]
    public class User
    {
        public string username { get; set; }

        public string password { get; set; }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty();

            RuleFor(x => x.password)
                .NotEmpty();
        }
    }

    public class AuthController : ApiController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        [HttpPost]
        public Object Login(User user)
        {
            if (ModelState.IsValid)
            {
                string key = "aFYbvBTvbGJf65ur"; //Secret key which will be used later during validation  

                var issuer = "http://www.envisio.com.my";  //normally this will be your site URL

                var obj = db.UserProfiles.Where(a => a.User_ID.Equals(user.username) && a.User_PW.Equals(user.password)).FirstOrDefault();
                if (obj != null)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    //Create a List of Claims, Keep claims name short    
                    var permClaims = new List<Claim>();
                    permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    permClaims.Add(new Claim("User_ID", obj.User_ID));
                    permClaims.Add(new Claim("Full_Name", obj.Full_Name));
                    permClaims.Add(new Claim("Phone", obj.Phone));

                    //Create Security Token object by giving required parameters    
                    var token = new JwtSecurityToken(issuer, //Issure    
                                    issuer,  //Audience    
                                    permClaims,
                                    expires: DateTime.Now.AddDays(1),
                                    signingCredentials: credentials);
                    var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                    return new { data = jwt_token };
                } else
                {
                    return BadRequest("Login Credential is invalid.");
                }
            } else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpGet]
        public Object me()
        {
            var identity = User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var username = claims.Where(p => p.Type == "User_ID").FirstOrDefault()?.Value;
            var fullName = claims.Where(p => p.Type == "Full_Name").FirstOrDefault()?.Value;
            var phone = claims.Where(p => p.Type == "Phone").FirstOrDefault()?.Value;
            return new
            {
                username = username,
                fullname = fullName,
                phone = phone
            };
        }
    }
}