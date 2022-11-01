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
public class DepartmentController : ControllerBase, ICrudController<DepartmentDto, Guid>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IValidator<DepartmentDto> _validator;

    public DepartmentController(IDepartmentRepository departmentRepository, IValidator<DepartmentDto> validator)
    {
        _departmentRepository = departmentRepository;
        _validator = validator;
    }


    [HttpGet("department-get-all")]
    public ActionResult<IList<DepartmentDto>> GetAll()
    {
        return Ok(_departmentRepository.GetAll().Select(x => new DepartmentDto
        {
            Id = x.Id,
            Description = x.Description
        }).ToList());
    }

    [HttpGet("department-get-by-id")]
    public ActionResult<DepartmentDto> GetById([FromQuery] Guid id)
    {
        var employeeType = _departmentRepository.GetById(id);

        return Ok(new DepartmentDto
        {
            Id = employeeType.Id,
            Description = employeeType.Description
        });
    }

    [HttpPost("department-create"),
        ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest),
        ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public ActionResult<Guid> Create([FromQuery] DepartmentDto data)
    {
        ValidationResult validationResult = _validator.Validate(data);
        
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(_departmentRepository.Create(new Department
        {
            Id = data.Id,
            Description = data.Description
        }));
    }

    [HttpPut("department-update"),
        ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public ActionResult<bool> Update([FromQuery] DepartmentDto data)
    {
        ValidationResult validationResult = _validator.Validate(data);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(_departmentRepository.Update(new Department
        {
            Id = data.Id,
            Description = data.Description
        }));
    }

    [HttpDelete("department-delete")]
    public ActionResult<bool> Delete([FromQuery] Guid id)
    {
        return Ok(_departmentRepository.Delete(id));
    } 
}
