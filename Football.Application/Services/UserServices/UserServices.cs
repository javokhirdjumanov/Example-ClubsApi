using Football.Application.DataTransferObjects.Users;
using Football.Domain.Entities;
using Football.Infrastructure.Repositories.UserRepositories;

namespace Football.Application.Services.UserServices
{
    public class UserServices : IUserServices
    {
        /// <summary>
        /// Constructor
        /// </summary>
        private readonly IUsersFactory usersFactory;
        private readonly IUserRepositiory userRepository;
        public UserServices(IUsersFactory usersFactory, IUserRepositiory userRepository)
        {
            this.usersFactory = usersFactory;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Create users
        /// </summary>
        /// <param name="userForCreationDto"></param>
        /// <returns></returns>
        public async ValueTask<UsersDTO> CreateUserAsync(UserForCreationDto userForCreationDto)
        {
            var newUser = this.usersFactory.MapToUser(userForCreationDto);

            var addedUser = await this.userRepository.InsertAsync(newUser);

            return this.usersFactory.MapToUserDto(addedUser);

        }

        /// <summary>
        /// get users and user that by id
        /// </summary>
        /// <returns></returns>
        public IQueryable<UsersDTO> RetrieveUsers()
        {
            var users = this.userRepository.SelectAll();

            return 
                users.Select(user => this.usersFactory.MapToUserDto(user));
        }
        public async ValueTask<UsersDTO> RetrieveUserByIdAsync(Guid userId)
        {
            var users = await this.userRepository.SelectByIdWithDetaialsAsync(
                expression: user => user.Id == userId,
                includes: new string[] { nameof(Users.Clubs) });

            return this.usersFactory.MapToUserDto(users);
        }

        /// <summary>
        /// Modifiyed users
        /// </summary>
        /// <param name="userForModificationDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask<UsersDTO> ModifyUserAsync(
            UserForModificationDto userForModificationDto)
        {
            var users = await this.userRepository.SelectByIdWithDetaialsAsync(
                expression: user => user.Id == userForModificationDto.userId,
                includes: new string[] { nameof(Users.Clubs) });

            this.usersFactory.MapToUser(users, userForModificationDto);

            var modifyuser = await this.userRepository.UpdateAsync(users);

            return this.usersFactory.MapToUserDto(modifyuser);
        }

        /// <summary>
        /// Remove users
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async ValueTask<UsersDTO> RemoveUserAsync(Guid userId)
        {
            var user = await this.userRepository.SelectByIdAsync(userId);

            var removeUser = await this.userRepository.DeleteAsync(user);

            return 
                this.usersFactory.MapToUserDto(removeUser);
        }
    }
}
