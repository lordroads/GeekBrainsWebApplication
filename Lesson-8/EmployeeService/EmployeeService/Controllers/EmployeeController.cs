using EmployeeService.Database.Data;
using EmployeeService.Models.Dto;
using EmployeeService.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase, ICrudController<EmployeeDto, int>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IValidator<EmployeeDto> _validator;

    public EmployeeController(IEmployeeRepository employeeRepository, IValidator<EmployeeDto> validator)
    {
        _employeeRepository = employeeRepository;
        _validator = validator;
    }


    [HttpGet("employee-get-all")]
    public ActionResult<IList<EmployeeDto>> GetAll()
    {
        return Ok(_employeeRepository.GetAll().Select(x => new EmployeeDto
        {
            Id = x.Id,
            DepartmentId = x.DepartmentId,
            EmployeeTypeID = x.EmployeeTypeID,
            FirstName = x.FirstName,
            Patronymic = x.Patronymic,
            Salary = x.Salary,
            Surname = x.Surname
        }).ToList());
    }

    [HttpGet("employee-get-by-id")]
    public ActionResult<EmployeeDto> GetById([FromQuery] int id)
    {
        var employee = _employeeRepository.GetById(id);

        return Ok(new EmployeeDto
        {
            Id = employee.Id,
            DepartmentId = employee.DepartmentId,
            EmployeeTypeID = employee.EmployeeTypeID,
            FirstName = employee.FirstName,
            Patronymic = employee.Patronymic,
            Salary = employee.Salary,
            Surname = employee.Surname
        });
    }

    [HttpPost("employee-create"),
        ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public ActionResult<int> Create([FromQuery] EmployeeDto data)
    {
        ValidationResult validationResult = _validator.Validate(data);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(_employeeRepository.Create(new Employee
        {
            Id = data.Id,
            DepartmentId = data.DepartmentId,
            EmployeeTypeID = data.EmployeeTypeID,
            FirstName = data.FirstName,
            Patronymic = data.Patronymic,
            Salary = data.Salary,
            Surname = data.Surname
        }));
    }

    [HttpPut("employee-update"),
        ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public ActionResult<bool> Update([FromQuery] EmployeeDto data)
    {
        ValidationResult validationResult = _validator.Validate(data);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(_employeeRepository.Update(new Employee
        {
            Id = data.Id,
            DepartmentId = data.DepartmentId,
            EmployeeTypeID = data.EmployeeTypeID,
            FirstName = data.FirstName,
            Patronymic = data.Patronymic,
            Salary = data.Salary,
            Surname = data.Surname
        }));
    }

    [HttpDelete("employee-delete")]
    public ActionResult<bool> Delete([FromQuery] int id)
    {
        return Ok(_employeeRepository.Delete(id));
    }
}
