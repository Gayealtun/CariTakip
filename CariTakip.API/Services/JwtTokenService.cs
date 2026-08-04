using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CariTakip.Entities.Models;
using Microsoft.IdentityModel.Tokens;

namespace CariTakip.API.Services;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(User user)
    {
        string key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "JWT anahtarı bulunamadı."
            );

        string issuer = _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException(
                "JWT issuer bilgisi bulunamadı."
            );

        string audience = _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException(
                "JWT audience bilgisi bulunamadı."
            );

        int expireMinutes =
            int.TryParse(
                _configuration["Jwt:ExpireMinutes"],
                out int minutes
            )
                ? minutes
                : 60;

        var claims = new List<Claim>
        {
            new(
                JwtRegisteredClaimNames.Sub,
                user.Id.ToString()
            ),
            new(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),
            new(
                ClaimTypes.Name,
                user.UserName),
            new(
                "firstName",
                user.FirstName),
            new(
                "lastName",
                user.LastName ),
            new(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString() )
        };

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
            );
        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}