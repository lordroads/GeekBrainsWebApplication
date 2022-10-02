using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeService.Database.Data;

[Table("Employees")]
public class Employee
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(Department))]
    public Guid DepartmentId { get; set; }

    [ForeignKey(nameof(EmployeeType))]
    public int EmployeeTypeID { get; set; }

    [Column, StringLength(255)]
    public string FirstName { get; set; }
    
    [Column, StringLength(255)]
    public string Surname { get; set; }

    [Column, StringLength(255)]
    public string Patronymic { get; set; }

    [Column(TypeName = "money")]
    public decimal Salary { get; set; }

    
    public Department Department { get; set; }
    public EmployeeType EmployeeType { get; set; }
}
