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
public class DictionariesController : ControllerBase, ICrudController<EmployeeTypeDto, int>
{
    private readonly IEmployeeTypeRepository _employeeTypeRepository;
    private readonly IValidator<EmployeeTypeDto> _validator;

    public DictionariesController(IEmployeeTypeRepository employeeTypeRepository, IValidator<EmployeeTypeDto> validator)
    {
        _employeeTypeRepository = employeeTypeRepository;
        _validator = validator;
    }


    [HttpGet("employee-type/get-all")]
    public ActionResult<IList<EmployeeTypeDto>> GetAll()
    {
        return Ok(_employeeTypeRepository.GetAll().Select(x => new EmployeeTypeDto 
        {
            Id = x.Id,
            Description = x.Description
        }).ToList());
    }

    [HttpGet("employee-type/get-by-id")]
    public ActionResult<EmployeeTypeDto> GetById([FromQuery] int id)
    {
        var employeeType = _employeeTypeRepository.GetById(id);

        if (employeeType != null)
        {
            return Ok(new EmployeeTypeDto
            {
                Id = employeeType.Id,
                Description = employeeType.Description
            });
        }

        return BadRequest(employeeType);
    }

    [HttpPost("employee-type/create"),
        ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public ActionResult<int> Create([FromQuery] EmployeeTypeDto data)
    {
        ValidationResult validationResult = _validator.Validate(data);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(_employeeTypeRepository.Create(new EmployeeType
        {
            Id = data.Id,
            Description = data.Description
        }));
    }

    [HttpPut("employee-type/update"),
        ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public ActionResult<bool> Update([FromQuery] EmployeeTypeDto data)
    {
        ValidationResult validationResult = _validator.Validate(data);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(_employeeTypeRepository.Update(new EmployeeType
        {
            Id = data.Id,
            Description = data.Description
        }));
    }

    [HttpDelete("employee-type/delete")]
    public ActionResult<bool> Delete([FromQuery] int id)
    {
        return Ok(_employeeTypeRepository.Delete(id));
    }
}
