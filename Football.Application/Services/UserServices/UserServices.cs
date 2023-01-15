using Football.Application.DataTransferObjects.Users;
using Football.Application.Extensions;
using Football.Application.Models;
using Football.Domain.Entities;
using Football.Infrastructure.Repositories.UserRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Football.Application.Services.UserServices
{
    public partial class UserServices : IUserServices
    {
        #region C T O R
        private readonly IUsersFactory usersFactory;
        private readonly IUserRepositiory userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserServices(IUsersFactory usersFactory, IUserRepositiory userRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.usersFactory = usersFactory;
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
        }
        #endregion

        /// <summary>
        /// Create storageUser
        /// </summary>
        public async ValueTask<UsersDTO> CreateUserAsync(UserForCreationDto userForCreationDto)
        {
            ValidateUserForCreationDto(userForCreationDto);

            var newUser = this.usersFactory.MapToUser(userForCreationDto);

            var addedUser = await this.userRepository.InsertAsync(newUser);

            return this.usersFactory.MapToUserDto(addedUser);
        }

        /// <summary>
        /// Get storageUser and storageUser that by id
        /// </summary>
        public IQueryable<UsersDTO> RetrieveUsers(QueryParametr queryParametr)
        {
            var users = this.userRepository
                .SelectAll()
                .ToPagesList(
                context: this.httpContextAccessor.HttpContext,
                pageSize: queryParametr.Page.Size,
                pageIndex: queryParametr.Page.Index);

            return 
                users.Select(user => this.usersFactory.MapToUserDto(user));
        }
        public async ValueTask<UsersDTO> RetrieveUserByIdAsync(Guid userId)
        {
            ValidateUserId(userId);

            var storageUsers = await this.userRepository.SelectByIdWithDetaialsAsync(
                expression: user => user.Id == userId,
                includes: new string[] { nameof(Users.Clubs) });

            ValidationsStorageUser(storageUsers, userId);

            return 
                this.usersFactory.MapToUserDto(storageUsers);
        }

        /// <summary>
        /// Modifiyed storageUser
        /// </summary>
        public async ValueTask<UsersDTO> ModifyUserAsync(UserForModificationDto userForModificationDto)
        {
            ValidateUserForModifiydDto(userForModificationDto);

            var storageUser = await this.userRepository.SelectByIdWithDetaialsAsync(
                expression: user => user.Id == userForModificationDto.userId,
                includes: new string[] { nameof(Users.Clubs) });

            ValidationsStorageUser(storageUser, userForModificationDto.userId);

            this.usersFactory.MapToUser(storageUser, userForModificationDto);

            var modifyuser = await this.userRepository.UpdateAsync(storageUser);

            return this.usersFactory.MapToUserDto(modifyuser);
        }

        /// <summary>
        /// Remove storageUser
        /// </summary>
        public async ValueTask<UsersDTO> RemoveUserAsync(Guid userId)
        {
            ValidateUserId(userId);

            var storageUser = await this.userRepository.SelectByIdAsync(userId);

            ValidationsStorageUser(storageUser, userId);

            var removeUser = await this.userRepository.DeleteAsync(storageUser);

            return 
                this.usersFactory.MapToUserDto(removeUser);
        }
    }
}
