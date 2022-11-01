using EmployeeService.Models.Dto;
using FluentValidation;

namespace EmployeeService.Models.Validators;

public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeDtoValidator()
    {
        RuleFor(x => x.DepartmentId)
            .NotNull();

        RuleFor(x => x.EmployeeTypeID)
            .NotNull();

        RuleFor(x => x.FirstName)
            .NotNull()
            .Length(1, 255);

        RuleFor(x => x.Surname)
            .Length(1, 255);

        RuleFor(x => x.Patronymic)
            .Length(1, 255);

        RuleFor(x => x.Salary)
            .NotNull();
    }
}
