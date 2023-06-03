using System.Text.RegularExpressions;
using AspNetWebApiWithMongoDb.Dtos;
using AspNetWebApiWithMongoDb.Models;
using FluentValidation;
using FluentValidation.Results;

namespace AspNetWebApiWithMongoDb.Validators;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    private readonly IValidator<UserAddressDto> _userAddressValidator;
    
    public UserUpdateValidator(IValidator<UserAddressDto> userAddressValidator)
    {
        _userAddressValidator = userAddressValidator;

        RuleFor(e => e.FirstName)
            .Must(NameValidation).WithMessage("FirstName is required!");

        RuleFor(e => e.LastName)
            .Must(NameValidation).WithMessage("LastName is required!");

        RuleFor(e => e.DateOfBirth)
            .NotNull().WithMessage("DateOfBirth is required!");

        RuleFor(e => e.Gender)
            .Must(e => e == Gender.MALE || e == Gender.FEMALE).WithMessage("Gender is required!");

        RuleFor(e => e.PhoneNumbers)
            .Must(PhoneNumbersValidation).WithMessage("PhoneNumber is required!");

        RuleFor(e => e.Addresses)
            .MustAsync(UserAddressValidationAsync).WithMessage("Address is required!");
    }
    
    private async Task<bool> UserAddressValidationAsync(IList<UserAddressDto> userAddresses,
        CancellationToken cancellationToken)
    {
        if (userAddresses == null || userAddresses.Count == 0)
        {
            return false;
        }

        foreach (UserAddressDto userAddress in userAddresses)
        {
            ValidationResult validationResult = await _userAddressValidator.ValidateAsync(userAddress, cancellationToken);
            
            if (!validationResult.IsValid)
            {
                return false;
            }
        }

        return true;
    }


    private bool PhoneNumbersValidation(string[] phoneNumbers)
    {
        if (phoneNumbers == null || phoneNumbers.Length == 0)
        {
            return false;
        }

        const string pattern = @"^((\+994|994)\d{9})|(0\d{9})|(\d{9})";
        Regex regex = new Regex(pattern);

        foreach (string phoneNumber in phoneNumbers)
        {
            if (regex.IsMatch(phoneNumber))
            {
                return true;
            }
        }

        return false;
    }

    private bool NameValidation(string name)
    {
        if (name == null)
        {
            return false;
        }

        const string pattern = "^[A-Z][a-zA-Z]*$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(name);
    }
}