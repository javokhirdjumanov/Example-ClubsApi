namespace Football.Application.DataTransferObjects.Authentication;
public record TokenDto(
    string accessToken,
    string? refleshToken,
    DateTime expireDate);
