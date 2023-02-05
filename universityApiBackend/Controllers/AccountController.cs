using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using universityApiBackend.Helpers;
using universityApiBackend.Models.DataModels;
using universityApiBackend.Models.Jwt;

namespace universityApiBackend.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly JwtSettings __jwtSettings;
        public AccountController(JwtSettings jwtSettings)
        {
            __jwtSettings = jwtSettings;
        }
        public IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id=1,
                Email="eliasmungui@gmail.com",
                Name="Admin",
                Password="Admin"
            },
            new User()
            {
                Id=2,
                Email="danielmartinez@gmail.com",
                Name="User1",
                Password="Admin"
            },
            new User()
            {
                Id=3,
                Email="joseangel@gmail.com",
                Name="User1",
                Password="Admin"
            }
        };
        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {
            try
            {
                var Token = new UserTokens();
                var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                if (Valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName=user.Name,
                        EmailId = user.Email,
                        Id= user.Id,
                        GuidId=Guid.NewGuid()

                    },__jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token);
                
            }catch(Exception ex)
            {
                throw new Exception("Get Token Error",ex);
            }

        }
        [HttpGet]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles ="Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}   
