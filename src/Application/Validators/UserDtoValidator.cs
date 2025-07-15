using FluentValidation;
using DotNetTemplate.Infrastructure.DTOs;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
        RuleFor(x => x.RoleId).NotEmpty().WithMessage("RoleId is required.");
    }
}
