namespace EmployeeService.Models.Requests;

public class AuthenticationRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}
