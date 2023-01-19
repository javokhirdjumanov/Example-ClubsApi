using Football.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Football.Infrastructure.Authentication;
public interface IJwtTokenHandler
{
    JwtSecurityToken GenerationAccessToken(Users user);
    string GenerateRefreshToken();
}
