using EmployeeService.Models.Dto;
using FluentValidation;

namespace EmployeeService.Models.Validators;

public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
{
    public DepartmentDtoValidator()
    {
        RuleFor(x => x.Description)
            .NotNull()
            .Length(3, 128);
    }
}