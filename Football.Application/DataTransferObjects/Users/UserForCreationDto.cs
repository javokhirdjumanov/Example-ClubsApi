namespace Football.Application.DataTransferObjects.Users;
public record UserForCreationDto(
    string firstname,
    string? lastname,
    string phonenumber,
    string email,
    string password,
    string clubname,
    string clubcountry,
    string? clubcity);
