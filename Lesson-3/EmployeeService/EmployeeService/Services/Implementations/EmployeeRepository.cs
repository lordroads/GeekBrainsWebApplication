using EmployeeService.Models;

namespace EmployeeService.Services.Implementations;

public class EmployeeRepository : IEmployeeRepository
{
    public int Create(Employee data)
    {
        return 0;
    }

    public bool Delete(int id)
    {
        return true;
    }

    public IList<Employee> GetAll()
    {
        return new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                DepartmentId = Guid.NewGuid(),
                EmployeeTypeID = 1,
                FirstName = "Employee FirstName #1",
                Surname = "Employee Surname #1",
                Patronymic = "Employee Surname #1",
                Salary = 1.00m
            },
            new Employee()
            {
                Id = 2,
                DepartmentId = Guid.NewGuid(),
                EmployeeTypeID = 2,
                FirstName = "Employee FirstName #2",
                Surname = "Employee Surname #2",
                Patronymic = "Employee Surname #2",
                Salary = 2.00m
            }
        };
    }

    public Employee GetById(int id)
    {
        return new Employee()
        {
            Id = id,
            DepartmentId = Guid.NewGuid(),
            EmployeeTypeID = 1,
            FirstName = "Employee FirstName #1",
            Surname = "Employee Surname #1",
            Patronymic = "Employee Surname #1",
            Salary = 1.00m
        };
    }

    public bool Update(Employee data)
    {
        return true;
    }
}
