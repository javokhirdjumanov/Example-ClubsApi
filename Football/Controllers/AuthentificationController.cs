using Football.Application.DataTransferObjects.Authentication;
using Football.Application.Services.Authentications;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Football.Api.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthentificationController : ControllerBase
{
    private readonly IAuthentication authenticationServices;
    public AuthentificationController(IAuthentication authenticationServices)
    {
        this.authenticationServices = authenticationServices;
    }

    [HttpPost]
    public async ValueTask<ActionResult<TokenDto>> LoginAsync(AuthenticationsDto authenticationsDto)
    {
        var tokenDto = await this.authenticationServices.LoginAsync(authenticationsDto);

        return Ok(tokenDto);
    }
}
