using FluentValidation;
using Football.Application.DataTransferObjects.Users;

namespace Football.Application.Validators.UsersValidation;
public class UserforCreateDtoValidation : AbstractValidator<UserForCreationDto>
{
	public UserforCreateDtoValidation()
	{
		RuleFor(user => user).NotNull();

		RuleFor(user => user.firstname)
			.MaximumLength(100)
			.NotEmpty();

		RuleFor(user => user.lastname)
			.MaximumLength(100);

		RuleFor(user => user.phonenumber)
			.MaximumLength(30)
			.NotEmpty();

		RuleFor(user => user.email)
			.MaximumLength(256)
			.EmailAddress()
			.NotEmpty();

		RuleFor(user => user.password)
			.MaximumLength(50)
			.NotEmpty();

		RuleFor(user => user.clubname) 
			.MaximumLength(50)
			.NotEmpty();

		RuleFor(user => user.clubcountry) 
			.MaximumLength(100)
			.NotEmpty();

		RuleFor(user => user.clubcity)
			.MaximumLength(100);
	}
}
