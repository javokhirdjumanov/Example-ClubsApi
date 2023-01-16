using Football.Application.DataTransferObjects.Authentication;

namespace Football.Application.Services.Authentications;
public interface IAuthentication
{
    Task<TokenDto> LoginAsync(AuthenticationsDto authenticationsDto);
}
