using Football.Application.DataTransferObjects.Users;
using Football.Domain.Entities;

namespace Football.Application.Services.UserServices
{
    public interface IUsersFactory
    {
        UsersDTO MapToUserDto(Users user);
        Users MapToUser(UserForCreationDto userForCreationDto);
        void MapToUser(Users storageUser, UserForModificationDto userForCreationDto);
    }
}
