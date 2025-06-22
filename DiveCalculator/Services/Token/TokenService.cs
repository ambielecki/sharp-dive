using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DiveCalculator.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DiveCalculator.Services.Token;

public class TokenService(IConfiguration config) : ITokenService
{
    public string GenerateToken(User user) {
        var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found in config");
        if (tokenKey.Length < 64) {
            throw new Exception("TokenKey is too short");       
        }
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim> {
            new (ClaimTypes.NameIdentifier, user.Username)
        };
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}