using EmployeeService.Models;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Requests;
using EmployeeService.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace EmployeeService.Controllers;

[ApiController, Route("api/[controller]"), Authorize]
public class AuthenticateController : Controller
{
    private readonly IAuthenticateService _authenticateService;
    private readonly IValidator<AuthenticationRequest> _validator;

    public AuthenticateController(IAuthenticateService authenticateService, IValidator<AuthenticationRequest> validator)
    {
        _authenticateService = authenticateService;
        _validator = validator;
    }

    [HttpPost("login"), 
        AllowAnonymous, 
        ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK),
        ProducesResponseType(typeof(IList<ValidationFailure>), StatusCodes.Status400BadRequest)]
    public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        ValidationResult validationResult = _validator.Validate(authenticationRequest);

        AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticationRequest);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        if (authenticationResponse.Status == AuthenticationStatus.Success)
        {
            Response.Headers.Add("X-Session-Token", authenticationResponse.Session.SessionToken);
        }

        return Ok(authenticationResponse);
    }

    [HttpGet("session"), ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
    public IActionResult GetSession()
    {
        //Authorization: Bearer XXXXXX... используется данная система для авторизации.
        //Header : Schema Token

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
