using EmployeeService.Database.Data;
using EmployeeService.Models.Dto;
using EmployeeService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase, ICrudController<EmployeeDto, int>
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
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

    [HttpPost("employee-create")]
    public ActionResult<int> Create([FromQuery] EmployeeDto data)
    {
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

    [HttpPut("employee-update")]
    public ActionResult<bool> Update([FromQuery] EmployeeDto data)
    {
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
