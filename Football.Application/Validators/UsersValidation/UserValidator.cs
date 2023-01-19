using FluentValidation;
using Football.Domain.Entities;

namespace Football.Application.Validators.UsersValidation;
public class UserValidator : AbstractValidator<Users>
{
	public UserValidator()
	{
		RuleFor(user => user)
			.NotNull();
	}
}
