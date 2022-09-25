using EmployeeService.Models;

namespace EmployeeService.Services.Implementations;

public class EmployeeTypeRepository : IEmployeeTypeRepository
{
    public int Create(EmployeeType data)
    {
        return 0;
    }

    public bool Delete(int id)
    {
        return true;
    }

    public IList<EmployeeType> GetAll()
    {
        return new List<EmployeeType>()
        {
            new EmployeeType{ 
                Id = 1, 
                Description = "Employee Type #1"
            },
            new EmployeeType{
                Id = 2,
                Description = "Employee Type #2"
            }
        };
    }

    public EmployeeType GetById(int id)
    {
        return new EmployeeType
        {
            Id = id,
            Description = "Employee Type #1"
        };
    }

    public bool Update(EmployeeType data)
    {
        return true;
    }
}
