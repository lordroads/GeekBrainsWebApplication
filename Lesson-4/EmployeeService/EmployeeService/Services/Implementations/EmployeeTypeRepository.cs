using EmployeeService.Database.Data;

namespace EmployeeService.Services.Implementations;

public class EmployeeTypeRepository : IEmployeeTypeRepository
{
    private readonly EmployeeServiceDbContext _dbContext;

    public EmployeeTypeRepository(EmployeeServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Create(EmployeeType data)
    {
        _dbContext.EmployeeTypes.Add(data);
        _dbContext.SaveChanges();

        return data.Id;
    }

    public bool Delete(int id)
    {
        EmployeeType employeeType = GetById(id);

        if (employeeType != null)
        {
            _dbContext.Remove(employeeType);
            _dbContext.SaveChanges();

            return true;
        }

        return false;
    }

    public IList<EmployeeType> GetAll()
    {
        return _dbContext.EmployeeTypes.ToList();
    }

    public EmployeeType GetById(int id)
    {
        return _dbContext.EmployeeTypes.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(EmployeeType data)
    {
        EmployeeType employeeType = GetById(data.Id);

        if (employeeType != null)
        {
            employeeType.Description = data.Description;
            var result = _dbContext.SaveChanges();

            return result > 0 ? true : false;
        }

        return false;
    }
}
