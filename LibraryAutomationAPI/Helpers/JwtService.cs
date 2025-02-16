using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraryAutomationAPI.Models;

namespace LibraryAutomationAPI.Helpers
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryMinutes;

        public JwtService(IConfiguration config)
        {
            _secret = config["JwtSettings:Secret"];
            _issuer = config["JwtSettings:Issuer"];
            _audience = config["JwtSettings:Audience"];
            _expiryMinutes = int.Parse(config["JwtSettings:ExpiryMinutes"]);
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName), // 📌 Kullanıcı adı sub claim'ine ekleniyor
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // 📌 Benzersiz token ID
                new Claim(ClaimTypes.Name, user.UserName) // 📌 Kullanıcı adı Name claim'ine de ekleniyor
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(_expiryMinutes);

            var token = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
