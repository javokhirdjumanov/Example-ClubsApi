using Football.Domain.Entities;
using Football.Domain.Enums;

namespace Football.Application.DataTransferObjects.Users
{
    public record UsersDTO(
        Guid Id,
        string firstname,
        string lastname,
        string phonenumber,
        string email,
        UserRoles role,
        ClubsDto? Clubs);
}
