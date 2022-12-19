using EmployeeService.Models.Dto;

namespace EmployeeService.Models.Requests;

public class AuthenticationResponse
{
    public AuthenticationStatus Status { get; set; }
    public SessionDto Session { get; set; }
}
