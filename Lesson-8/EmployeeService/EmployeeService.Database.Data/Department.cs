using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeService.Database.Data;

[Table("Departments")]
public class Department
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column(TypeName = "nvarchar(128)")]
    public string Description { get; set; }

    [InverseProperty(nameof(Employee.Department))]
    public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
}
