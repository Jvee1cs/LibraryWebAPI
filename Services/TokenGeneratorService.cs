using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Librarykuno.Models;
using Microsoft.IdentityModel.Tokens;

namespace Librarykuno.Services
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(Member member);
    }

    public class TokenGeneratorService : ITokenGeneratorService
    {
        private readonly IConfiguration _config;

        public TokenGeneratorService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(Member member)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
                new Claim(ClaimTypes.Name, member.Name),
                new Claim(ClaimTypes.Email, member.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}