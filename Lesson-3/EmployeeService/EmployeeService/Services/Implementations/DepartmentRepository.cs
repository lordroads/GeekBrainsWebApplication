using EmployeeService.Models;

namespace EmployeeService.Services.Implementations;

public class DepartmentRepository : IDepartmentRepository
{
    public Guid Create(Department data)
    {
        return Guid.NewGuid();
    }

    public bool Delete(Guid id)
    {
        return true;
    }

    public IList<Department> GetAll()
    {
        return new List<Department>()
        {
            new Department(){ Id = Guid.NewGuid(), Description = "Department #1" },
            new Department(){ Id = Guid.NewGuid(), Description = "Department #2" }
        };
    }

    public Department GetById(Guid id)
    {
        return new Department() { Id = Guid.NewGuid(), Description = "Department #1" };
    }

    public bool Update(Department data)
    {
        return true;
    }
}
