using Football.Application.DataTransferObjects.Authentication;

namespace Football.Application.Services.Authentications;
public interface IAuthentications
{
    Task<TokenDto> AccessTokenAsync(AuthenticationsDto authenticationsDto);
    Task<TokenDto> RefreshTokenAsync(RefreshTokenDto refleshTokenDto);
}
