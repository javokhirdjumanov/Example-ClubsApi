using FluentValidation.Results;
using Football.Application.DataTransferObjects.Users;
using Football.Application.Validators.UsersValidation;
using Football.Domain.Entities;
using Football.Domain.Exceptions;
using System.Text.Json;

namespace Football.Application.Services.UserServices;
public partial class UserServices
{
    /// <summary>
    /// Validation for userId that userid is null
    /// </summary>
    public void ValidateUserId(Guid userId)
    {
        if (userId == default)
        {
            throw new ValidationExceptions($"The given userId is invalid: {userId}");
        }
    }

    /// <summary>
    /// Validation for storage user is null
    /// </summary>
    public void ValidationsStorageUser(Users storageUser, Guid userId)
    {
        if (storageUser == null)
        {
            throw new NotFoundExcaptions($"Couldn't find user with given id: {userId}");
        }
    }

    /// <summary>
    /// If it doesn't pass Validation on creation
    /// </summary>
    public void ValidateUserForCreationDto(UserForCreationDto userForCreationDto)
    {
        var validateResult = 
            new UserforCreateDtoValidation().Validate(userForCreationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validateResult);
    }

    /// <summary>
    /// If it doesn't pass Validation on update
    /// </summary>
    public void ValidateUserForModifiydDto(UserForModificationDto userForModificationDto)
    {
        var validationResult = new UserForModificationDtoValidation().Validate(userForModificationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validationResult);
    }

    /// <summary>
    /// If it doesn't pass Validation of Base
    /// </summary>
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
