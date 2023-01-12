namespace Football.Application.DataTransferObjects.Users
{
    public record UserForModificationDto(
        Guid userId,
        string? firstname,
        string? lastname,
        string? phonenumber,
        string? clubname,
        string? clubcountry,
        string? clubcity);
}
