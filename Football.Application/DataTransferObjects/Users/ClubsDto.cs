namespace Football.Application.DataTransferObjects.Users
{
    public record ClubsDto(
        string clubname,
        string clubcountry,
        string? clubcity);
}
