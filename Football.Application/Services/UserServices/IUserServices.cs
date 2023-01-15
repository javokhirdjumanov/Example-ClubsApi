using Football.Application.DataTransferObjects.Users;
using Football.Application.Models;

namespace Football.Application.Services.UserServices
{
    public interface IUserServices
    {
        ValueTask<UsersDTO> CreateUserAsync(UserForCreationDto userForCreationDto);
        IQueryable<UsersDTO> RetrieveUsers(QueryParametr queryParametr);
        ValueTask<UsersDTO> RetrieveUserByIdAsync(Guid userId);
        ValueTask<UsersDTO> ModifyUserAsync(UserForModificationDto userForModificationDto);
        ValueTask<UsersDTO> RemoveUserAsync(Guid userId);
    }
}
