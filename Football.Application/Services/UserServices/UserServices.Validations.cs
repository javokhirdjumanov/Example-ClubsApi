using FluentValidation.Results;
using Football.Application.DataTransferObjects.Users;
using Football.Application.Validators.UsersValidation;
using Football.Domain.Entities;
using Football.Domain.Exceptions;
using System.Text.Json;

namespace Football.Application.Services.UserServices;
public partial class UserServices
{
    public void ValidateUserId(Guid userId)
    {
        if (userId == default)
        {
            throw new ValidationExceptions($"The given userId is invalid: {userId}");
        }
    }
    public void ValidationsStorageUser(Users storageUser, Guid userId)
    {
        if (storageUser == null)
        {
            throw new NotFoundExcaptions($"Couldn't find user with given id: {userId}");
        }
    }
    public void ValidateUserForCreationDto(UserForCreationDto userForCreationDto)
    {
        var validateResult = 
            new UserforCreateDtoValidation().Validate(userForCreationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validateResult);
    }
    public void ValidateUserForModifiydDto(UserForModificationDto userForModificationDto)
    {
        var validationResult = new UserForModificationDtoValidation().Validate(userForModificationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validationResult);
    }
    private static void ThrowValidationExceptionIfValidationIsInvalid(ValidationResult validationResult)
    {
        if(validationResult.IsValid) 
        {
            return;
        }

        var errors = JsonSerializer.Serialize(validationResult.Errors
            .Select(error => new
            {
                PropertyName = error.PropertyName,
                ErrorMessage = error.ErrorMessage,
                AttemptedValue = error.AttemptedValue
            }));

        throw new ValidationExceptions(errors);
    }
}
