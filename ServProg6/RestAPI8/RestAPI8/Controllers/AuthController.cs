using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestAPI8.Data;

namespace RestAPI8.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;
        private SymmetricSecurityKey Key { get; init; }

        public AuthController(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
            Key = new(Encoding.UTF8.GetBytes(configuration["Auth:KEY"]!));
            Console.WriteLine(Key);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(string login, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Name == login && u.Password == password);
            if (user is null)
                return NotFound();

            var claims = new List<Claim> { new(ClaimTypes.Name, user.Name), new(ClaimTypes.Role, user.Role) };

            var jwt = new JwtSecurityToken(
                    issuer: configuration["Auth:ISSUER"],
                    audience: configuration["Auth:AUDIENCE"],
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpGet]
        public ActionResult<string> HelloAspNet() => Ok("Текст");
    }
}
