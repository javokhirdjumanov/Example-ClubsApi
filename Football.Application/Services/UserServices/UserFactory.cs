using Football.Application.DataTransferObjects.Users;
using Football.Domain.Entities;
using Football.Domain.Enums;

namespace Football.Application.Services.UserServices
{
    public class UserFactory : IUsersFactory
    {
        /// <summary>
        /// Malumotni User tipiga o'girish
        /// </summary>
        /// <param name="userForCreationDto"></param>
        /// <returns></returns>
        public Users MapToUser(UserForCreationDto userForCreationDto)
        {
            return new Users
            {
                FirstName = userForCreationDto.firstname,
                LastName = userForCreationDto.lastname,
                PhoneNumber = userForCreationDto.phonenumber,
                Email = userForCreationDto.email,
                PasswordHash = userForCreationDto.password,
                Salt = Guid.NewGuid().ToString(),
                Clubs = new Clubs
                {
                    ClubName = userForCreationDto.clubname,
                    ClubCountry = userForCreationDto.clubcountry,
                    ClubCity = userForCreationDto.clubcity,
                },
                Role = UserRoles.FootballPlayers
            };
        }

        /// <summary>
        /// User malumotlarini o'zgartirish uchun
        /// </summary>
        /// <param name="storageUser"></param>
        /// <param name="userForCreationDto"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void MapToUser(Users storageUser, UserForModificationDto userForCreationDto)
        {
            storageUser.FirstName = userForCreationDto.firstname ?? storageUser.FirstName;
            storageUser.LastName = userForCreationDto.lastname;
            storageUser.PhoneNumber = userForCreationDto.phonenumber ?? storageUser.PhoneNumber;
            storageUser.CreateAt = DateTime.UtcNow;

            storageUser.Clubs = storageUser.Clubs ?? new Clubs();
            storageUser.Clubs.ClubName = userForCreationDto.clubname ?? storageUser.Clubs.ClubName;
            storageUser.Clubs.ClubCountry = userForCreationDto.clubcountry ?? storageUser.Clubs.ClubCountry;
            storageUser.Clubs.ClubCity = userForCreationDto.clubcity;
        }

        /// <summary>
        /// Malumotni userDto tipiga o'girish
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UsersDTO MapToUserDto(Users user)
        {
            ClubsDto? clubsDto = default;

            if(user.Clubs is not null) 
            {
                clubsDto = new ClubsDto(
                    user.Clubs.ClubName,
                    user.Clubs.ClubCountry,
                    user.Clubs.ClubCity);
            }

            return new UsersDTO(
                user.Id,
                user.FirstName,
                user.LastName!,
                user.PhoneNumber,
                user.Email,
                user.Role,
                clubsDto);
        }
    }
}
