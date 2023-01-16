using Football.Domain.Entities;
using Football.Domain.Enums;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Football.Infrastructure.Authentication;
public class JwtTokenHandler : IJwtTokenHandler
{
    /// <summary>
    /// C T O R
    /// </summary>
    private readonly Jwtoption jwtoption;
    public JwtTokenHandler(IOptions<Jwtoption> options)
    {
        this.jwtoption = options.Value;
    }

    /// <summary>
    /// Generation Jwt Token
    /// </summary>
    public JwtSecurityToken GenerationJwtToken(Users user)
    {
        var claims = new List<Claim>()
        {
            new Claim(CustomClaimNames.Id, user.Id.ToString()),
            new Claim(CustomClaimNames.Email, user.Email),
            new Claim(CustomClaimNames.Role, Enum.GetName<UserRoles>(user.Role))
        };

        var authSingningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.jwtoption.SecretKey));

        var token = new JwtSecurityToken(
            issuer: this.jwtoption.Issuer,
            audience: this.jwtoption.Audience,
            expires: DateTime.UtcNow.AddMinutes(this.jwtoption.ExpirationInMinutes),
            claims: claims,
            signingCredentials: new SigningCredentials(
                key: authSingningKey,
                algorithm: SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}
