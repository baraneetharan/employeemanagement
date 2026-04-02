// EmployeeManagement.Application/Features/Employees/Validators/CreateEmployeeCommandValidator.cs
using FluentValidation;
using EmployeeManagement.Application.Features.Employees.Commands;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100);

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeAtLeast18YearsOld).WithMessage("Employee must be at least 18 years old.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than zero.");
    }

    private bool BeAtLeast18YearsOld(DateTime dateOfBirth)
    {
        return DateTime.UtcNow.Year - dateOfBirth.Year >= 18;
    }
}