using EmployeeService.Database.Data;

namespace EmployeeService.Services.Implementations;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly EmployeeServiceDbContext _dbContext;

    public DepartmentRepository(EmployeeServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Create(Department data)
    {
        data.Id = Guid.NewGuid();

        _dbContext.Departments.Add(data);
        _dbContext.SaveChanges();

        return data.Id;
    }

    public bool Delete(Guid id)
    {
        Department department = GetById(id);
        if (department != null)
        {
            _dbContext.Remove(department);
            _dbContext.SaveChanges();

            return true;
        }
        return false;
    }

    public IList<Department> GetAll()
    {
        return _dbContext.Departments.ToList();
    }

    public Department GetById(Guid id)
    {
        return _dbContext.Departments.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(Department data)
    {
        Department department = GetById(data.Id);

        if (department != null)
        {
            department.Description = data.Description;
            var result = _dbContext.SaveChanges();
            
            return result > 0 ? true : false;
        }

        return false;
    }
}
