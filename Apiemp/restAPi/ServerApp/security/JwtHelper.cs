using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
namespace ServerApp.security;
public class JwtHelper
{
    private readonly static SecurityKey key = new SymmetricSecurityKey(RandomNumberGenerator.GetBytes(32));

    public static string CreateToken(string Username)
    {
        var token = new JwtSecurityToken(
            claims: [new Claim(ClaimTypes.Name , Username)],
            expires: DateTime.UtcNow.AddMinutes(20),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static void ValidateToken(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = key
        };
    }


}