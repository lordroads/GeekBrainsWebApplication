using EmployeeService.Database.Data;
using EmployeeService.Models.Dto;
using EmployeeService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase, ICrudController<DepartmentDto, Guid>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
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

    [HttpPost("department-create")]
    public ActionResult<Guid> Create([FromQuery] DepartmentDto data)
    {
        return Ok(_departmentRepository.Create(new Department
        {
            Id = data.Id,
            Description = data.Description
        }));
    }

    [HttpPut("department-update")]
    public ActionResult<bool> Update([FromQuery] DepartmentDto data)
    {
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
