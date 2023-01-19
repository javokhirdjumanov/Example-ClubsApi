using Football.Application.DataTransferObjects.Authentication;
using Football.Application.Services.Authentications;
using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthentificationController : ControllerBase
{
    /// <summary>
    /// C o n s t r u c t o r
    /// </summary>
    private readonly IAuthentications authenticationServices;
    public AuthentificationController(IAuthentications authenticationServices)
    {
        this.authenticationServices = authenticationServices;
    }

    /// <summary>
    /// P o s t   Access Token
    /// </summary>
    [HttpPost]
    public async ValueTask<ActionResult<TokenDto>> LoginAsync(AuthenticationsDto authenticationsDto)
    {
        var tokenDto = await this.authenticationServices.AccessTokenAsync(authenticationsDto);

        return Ok(tokenDto);
    }

    /// <summary>
    /// P o s t   Refresh Token
    /// </summary>
    [HttpPost("refresh-token")]
    public async ValueTask<ActionResult<TokenDto>> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var tokenDto = await this.authenticationServices.RefreshTokenAsync(refreshTokenDto);

        return Ok(tokenDto);
    }
}
