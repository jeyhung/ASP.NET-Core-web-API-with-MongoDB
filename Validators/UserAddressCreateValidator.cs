using AspNetWebApiWithMongoDb.Dtos;
using FluentValidation;

namespace AspNetWebApiWithMongoDb.Validators;

public class UserAddressCreateValidator : AbstractValidator<UserAddressCreateDto>
{
    public UserAddressCreateValidator()
    {
        RuleFor(e => e.Country)
            .NotNull().WithMessage("Country is required!");

        RuleFor(e => e.City)
            .NotNull().WithMessage("City is required!");

        RuleFor(e => e.AddressLine1)
            .NotNull().WithMessage("AddressLine1 is required!");
    }
}