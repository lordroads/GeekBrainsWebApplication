using EmployeeService.Controllers;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Requests;
using EmployeeService.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace EmployeeServiceTests;

public class AuthenticateControllerTEsts
{
    private readonly AuthenticateController _controller;
    private readonly Mock<IAuthenticateService> _mockAuthenticateService;
    private readonly Mock<IValidator<AuthenticationRequest>> _mockValidator;

    public static IEnumerable<object[]> AuthenticateDataTestMemberData
    {
        get
        {
            return new[]
            {
                new object[] { new AuthenticationRequest { Login = "test1@test.test", Password = "test12345"} },
            };
        }
    }

    public AuthenticateControllerTEsts()
    {
        _mockAuthenticateService = new Mock<IAuthenticateService>();
        _mockValidator = new Mock<IValidator<AuthenticationRequest>>(MockBehavior.Strict);
        _mockValidator.
            Setup(x => x.Validate(It.IsAny<AuthenticationRequest>()))
            .Returns(new ValidationResult());

        _controller = new AuthenticateController(_mockAuthenticateService.Object, _mockValidator.Object);
    }

    [Theory]
    [MemberData(nameof(AuthenticateDataTestMemberData))]
    public void LoginTets(AuthenticationRequest authenticationRequest)
    {
        _mockAuthenticateService.Setup(service => service.Login(It.IsAny<AuthenticationRequest>())).Returns(
            new AuthenticationResponse 
            { 
                Session = 
                new SessionDto 
                { 
                    SessionId = 1, 
                    SessionToken = "TEST" 
                }  
            }
            );

        var result = _controller.Login(authenticationRequest);

        _mockAuthenticateService.Verify(service => service.Login(It.IsAny<AuthenticationRequest>()), Times.Once());
    }

    [Fact]
    public void GetSessionTest()
    {
    }
}
