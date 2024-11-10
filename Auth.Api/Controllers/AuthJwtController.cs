using Auth.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthJwtController : ControllerBase
    {
        private readonly string _secretKey = "ThisIsASecretKeyThatIs256BitsLong123"; // Debe ser más segura en producción
        private readonly string _issuer = "AuthServer";
        private readonly string _audience = "AuthClient";

        // POST api/authjwt/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto model)
        {
            if (model.Username == "admin" && model.Password == "password")  // Validación simple
            {
                var token = GenerateJwtToken(model.Username);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("userType", "admin") // Puedes agregar más roles o claims según lo necesites
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
