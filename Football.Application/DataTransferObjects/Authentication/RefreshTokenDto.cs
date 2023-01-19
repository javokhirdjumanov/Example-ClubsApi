namespace Football.Application.DataTransferObjects.Authentication;
public record RefreshTokenDto(
    string accessToken,
    string refreshToken);
