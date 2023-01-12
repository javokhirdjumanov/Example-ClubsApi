using Football.Application.DataTransferObjects.Users;

namespace Football.Application.Services.UserServices
{
    public interface IUserServices
    {
        ValueTask<UsersDTO> CreateUserAsync(UserForCreationDto userForCreationDto);
        IQueryable<UsersDTO> RetrieveUsers();
        ValueTask<UsersDTO> RetrieveUserByIdAsync(Guid userId);
        ValueTask<UsersDTO> ModifyUserAsync(UserForModificationDto userForModificationDto);
        ValueTask<UsersDTO> RemoveUserAsync(Guid userId);
    }
}
