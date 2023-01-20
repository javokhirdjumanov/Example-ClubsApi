using Football.Application.DataTransferObjects.Authentication;
using Football.Domain.Entities;
using Football.Domain.Exceptions;

namespace Football.Application.Services.Authentications;
public partial class Authentications
{
    public void ValidationForStorageUser(Users user, Guid userId)
    {
        if(user == null)
        {
            throw new NotFoundExcaptions($"Couldn't find user with given id: {userId}");
        }
    }
    public void ValidationEqualRefreshToken(Users storageUser, RefreshTokenDto refreshTokenDto)
    {
        if (!storageUser.RefreshToken.Equals(refreshTokenDto.refreshToken))
        {
            throw new ValidationExceptions("Refresh token is not valid");
        }
    }
    public void ValidationExpireDateForRefreshToken(Users storageUser)
    {
        if (storageUser.RefreshTokenExpireDate <= DateTime.UtcNow)
        {
            throw new ValidationExceptions("Refresh token has already been expired");
        }
    }
}
