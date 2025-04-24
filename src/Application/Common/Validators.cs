using DotNetTemplate.Application.DTOs;
using FluentValidation;

namespace DotNetTemplate.Application.Common;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).MinimumLength(6);
    }
}

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class CreateUserContactDtoValidator : AbstractValidator<CreateUserContactDto>
{
    public CreateUserContactDtoValidator()
    {
        RuleFor(x => x.Phone).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
