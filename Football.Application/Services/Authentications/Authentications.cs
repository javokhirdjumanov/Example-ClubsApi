using Football.Application.DataTransferObjects.Authentication;
using Football.Infrastructure.Authentication;
using Football.Infrastructure.Repositories.UserRepositories;
using System.IdentityModel.Tokens.Jwt;

namespace Football.Application.Services.Authentications;
public class Authentications : IAuthentication
{
    /// <summary>
    /// C O N S T R U C T O R
    /// </summary>
    private readonly IUserRepositiory userRepositiory;
    private readonly IJwtTokenHandler jwtTokenHandler;
    public Authentications(IUserRepositiory userRepositiory, IJwtTokenHandler jwtTokenHandler)
    {
        this.userRepositiory = userRepositiory;
        this.jwtTokenHandler = jwtTokenHandler;
    }

    /// <summary>
    /// L O G I N
    /// </summary>
    public async Task<TokenDto> LoginAsync(AuthenticationsDto authenticationsDto)
    {
        var storegeUser = await this.userRepositiory.SelectByIdWithDetaialsAsync(
            expression: user => 
            user.Email == authenticationsDto.email && user.PasswordHash == authenticationsDto.password,

            includes: Array.Empty<string>()
            );

        var token = this.jwtTokenHandler.GenerationJwtToken(storegeUser);

        return new TokenDto(
            accessToken: new JwtSecurityTokenHandler().WriteToken(token),
            refleshToken: null,
            expireDate: token.ValidTo
            );
    }
}
