using EmployeeService.Models.Dto;
using FluentValidation;

namespace EmployeeService.Models.Validators;

public class EmployeeTypeDtoValidator : AbstractValidator<EmployeeTypeDto>
{
    public EmployeeTypeDtoValidator()
    {
        RuleFor(x => x.Description)
            .NotNull()
            .Length(3, 128);
    }
}