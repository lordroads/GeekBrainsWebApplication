using EmployeeService.Database.Data;

namespace EmployeeService.Services.Implementations;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeServiceDbContext _dbContext;

    public EmployeeRepository(EmployeeServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Create(Employee data)
    {
        if (IsDepartmentById(data.DepartmentId) & IsEmployeeTypeById(data.EmployeeTypeID))
        {
            _dbContext.Employees.Add(data);
            _dbContext.SaveChanges();

            return data.Id;
        }
        
        return -1;
    }

    public bool Delete(int id)
    {
        Employee employee = GetById(id);

        if (employee != null)
        {
            _dbContext.Remove(employee);
            _dbContext.SaveChanges();

            return true;
        }

        return false;
    }

    public IList<Employee> GetAll()
    {
        return _dbContext.Employees.ToList();
    }

    public Employee GetById(int id)
    {
        return _dbContext.Employees.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(Employee data)
    {
        Employee employee = GetById(data.Id);

        if (employee != null)
        {
            if (IsDepartmentById(data.DepartmentId))
            {
                employee.DepartmentId = data.DepartmentId;
            }
            if (IsEmployeeTypeById(data.EmployeeTypeID))
            {
                employee.EmployeeTypeID = data.EmployeeTypeID;
            }

            employee.FirstName = data.FirstName;
            employee.Surname = data.Surname;
            employee.Patronymic = data.Patronymic;
            employee.Salary = data.Salary;

            var result = _dbContext.SaveChanges();

            return result > 0 ? true : false;
        }

        return false;
    }

    private bool IsDepartmentById(Guid id)
    {
        return _dbContext.Departments.FirstOrDefault(x => x.Id == id) != null;
    }
    private bool IsEmployeeTypeById(int id)
    {
        return _dbContext.EmployeeTypes.FirstOrDefault(x => x.Id == id) != null;
    }
}
