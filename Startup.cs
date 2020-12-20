using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;

[assembly: OwinStartup(typeof(Preveld.Startup))]

namespace Preveld
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://www.envisio.com.my", //some string, normally web url,  
                        ValidAudience = "http://www.envisio.com.my",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aFYbvBTvbGJf65ur"))
                    }
                });
        }
    }
}
