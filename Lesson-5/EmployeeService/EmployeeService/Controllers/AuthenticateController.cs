using EmployeeService.Models;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Requests;
using EmployeeService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace EmployeeService.Controllers;

[ApiController, Route("api/[controller]"), Authorize]
public class AuthenticateController : Controller
{
    private readonly IAuthenticateService _authenticateService;

    public AuthenticateController(IAuthenticateService authenticateService)
    {
        _authenticateService = authenticateService;
    }

    [HttpPost("login"), AllowAnonymous]
    public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);

        if (authenticationResponse.Status == AuthenticationStatus.Success)
        {
            Response.Headers.Add("X-Session-Token", authenticationResponse.Session.SessionToken);
        }

        return Ok(authenticationResponse);
    }

    [HttpGet("session")]
    public IActionResult GetSession()
    {
        //Authorization: Bearer XXXXXX... используется данная система для авторизации.

        var authorizationHeader = Request.Headers[HeaderNames.Authorization];

        if (AuthenticationHeaderValue.TryParse(authorizationHeader, out var authorizationValue))
        {
            var schema = authorizationValue.Scheme; //Bearer
            var sessionToken = authorizationValue.Parameter; // Token

            if (string.IsNullOrEmpty(sessionToken))
            {
                return Unauthorized();
            }

            SessionDto sessionDto = _authenticateService.GetSession(sessionToken);

            if (sessionDto == null)
            {
                return Unauthorized();
            }

            return Ok(sessionDto);
        }

        return Unauthorized();
    }
}
