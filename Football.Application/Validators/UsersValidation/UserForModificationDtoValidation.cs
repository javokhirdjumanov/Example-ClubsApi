using FluentValidation;
using Football.Application.DataTransferObjects.Users;

namespace Football.Application.Validators.UsersValidation;
public class UserForModificationDtoValidation : AbstractValidator<UserForModificationDto>
{
	public UserForModificationDtoValidation()
	{
		RuleFor(user => user)
			.NotNull();

		RuleFor(user => user.firstname)
			.MaximumLength(50);

        RuleFor(user => user.lastname)
            .MaximumLength(50);

        RuleFor(user => user.phonenumber)
            .MaximumLength(50);

        RuleFor(user => user.clubcountry)
            .MaximumLength(100);

        RuleFor(user => user.clubname)
            .MaximumLength(50);

        RuleFor(user => user.clubcity)
            .MaximumLength(100);

    }
}
