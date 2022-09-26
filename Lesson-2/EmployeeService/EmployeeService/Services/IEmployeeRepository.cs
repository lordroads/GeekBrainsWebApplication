using EmployeeService.Models;

namespace EmployeeService.Services;

public interface IEmployeeRepository : IRepository<Employee, int>
{

}
