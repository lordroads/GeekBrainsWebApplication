using EmployeeService.Models;
using EmployeeService.Models.Dto;
using EmployeeService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DictionariesController : ControllerBase, ICrudController<EmployeeTypeDto, int>
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public DictionariesController(IEmployeeTypeRepository employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
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

            return Ok(new EmployeeTypeDto
            {
                Id = employeeType.Id,
                Description = employeeType.Description
            });
        }

        [HttpPost("employee-type/create")]
        public ActionResult<int> Create([FromQuery] EmployeeTypeDto data)
        {
            return Ok(_employeeTypeRepository.Create(new EmployeeType
            {
                Id = data.Id,
                Description = data.Description
            }));
        }

        [HttpPut("employee-type/update")]
        public ActionResult<bool> Update([FromQuery] EmployeeTypeDto data)
        {
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
}
