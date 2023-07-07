using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinvoiceWebhook.BUS;
using MinvoiceWebhook.DAL;
using MinvoiceWebhook.MOD;
using MinvoiceWebhook.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinvoiceWebhook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;
        private string _key => _configuration.GetValue<string>("AppSettings:SecretKey");
        public AccountController(IConfiguration configuration, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _configuration = configuration;
            _appSettings = optionsMonitor.CurrentValue;

        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_key);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);


            return jwtTokenHandler.WriteToken(token);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(AccountMOD login)
        {
            if (login == null) return BadRequest();
            var Result = new AccountBUS().LoginBUS(login);

            List<Claim> claims = new List<Claim>();
            if (Result != null && Result.Status == 1)
            {
                return Ok(new
                {
                    Result,
                    token = GenerateToken(claims)
                });
            }
            else
            {
                return Ok(new {Result} );
            }
            return NotFound();
        } 
    }
}
