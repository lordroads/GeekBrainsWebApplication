using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Database.Data;

public class EmployeeServiceDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<EmployeeType> EmployeeTypes { get; set; }

    public EmployeeServiceDbContext(DbContextOptions options) : base(options)
    {
    }
}
