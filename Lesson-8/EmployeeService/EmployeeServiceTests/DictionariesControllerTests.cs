using EmployeeService.Controllers;
using EmployeeService.Database.Data;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Validators;
using EmployeeService.Services;
using EmployeeServiceTests.Data;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections.Generic;
using Xunit;
using Xunit.Priority;

namespace EmployeeServiceTests;


public class DictionariesControllerTests
{
    private readonly DictionariesController _controller;
    private readonly Mock<IEmployeeTypeRepository> _mockEmployeeTypeRepository;
    private readonly Mock<IValidator<EmployeeTypeDto>> _mockValidator;

    public DictionariesControllerTests()
    {
        _mockEmployeeTypeRepository = new Mock<IEmployeeTypeRepository>();
        _mockValidator = new Mock<IValidator<EmployeeTypeDto>>(MockBehavior.Strict);
        _mockValidator.
            Setup(x => x.Validate(It.IsAny<EmployeeTypeDto>()))
            .Returns(new ValidationResult());


        _controller = new DictionariesController(_mockEmployeeTypeRepository.Object, _mockValidator.Object);
    }

    [Fact]
    public void GetAll()
    {
        //[1] Подготовка
        _mockEmployeeTypeRepository.Setup(repo => repo.GetAll()).Returns(new List<EmployeeType>());

        //[2] Проведение теста
        var result = _controller.GetAll();

        //[3] Проверка теста
        _mockEmployeeTypeRepository.Verify(repo => repo.GetAll(), Times.Once());
    }


    [Theory, Priority(1)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetById(int id)
    {
        //[1] Подготовка
        _mockEmployeeTypeRepository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new EmployeeType());

        //[2] Проведение теста
        var result = _controller.GetById(id);

        //[3] Проверка теста
        _mockEmployeeTypeRepository.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once());
    }

    [Theory]
    [ClassData(typeof(EmployeeTypeDtoDataTest))] //Использование класса для передачи данных
    public void Create(EmployeeTypeDto data)
    {
        //[1] Подготовка
        _mockEmployeeTypeRepository.Setup(repo => repo.Create(It.IsAny<EmployeeType>())).Verifiable();

        //[2] Проведение теста
        var result = _controller.Create(data);

        //[3] Проверка теста
        _mockEmployeeTypeRepository.Verify(repo => repo.Create(It.IsAny<EmployeeType>()), Times.Once());
    }

    [Theory]
    [MemberData(nameof(EmployeeTypeDtoDataTestMemberData))] //ИСпользование св-ва для передачи данных
    public void Update(EmployeeTypeDto data)
    {
        //[1] Подготовка
        _mockEmployeeTypeRepository.Setup(repo => repo.Update(It.IsAny<EmployeeType>())).Verifiable();

        //[2] Проведение теста
        var result = _controller.Update(data);

        //[3] Проверка теста
        _mockEmployeeTypeRepository.Verify(repo => repo.Update(It.IsAny<EmployeeType>()), Times.Once());
    }
    public static IEnumerable<object[]> EmployeeTypeDtoDataTestMemberData
    {
        get
        {
            return new[]
            {
                new object[] { new EmployeeTypeDto { Id = 1, Description = "test1"} },
                new object[] { new EmployeeTypeDto { Id = 2, Description = "test2"} },
                new object[] { new EmployeeTypeDto { Id = 3, Description = "test3" } }
            };
        }
    }

    [Theory, Priority(2)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Delete(int id)
    {
        //[1] Подготовка
        _mockEmployeeTypeRepository.Setup(repo => repo.Delete(It.IsAny<int>())).Verifiable();

        //[2] Проведение теста
        var result = _controller.Delete(id);

        //[3] Проверка теста
        _mockEmployeeTypeRepository.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once());
    }
}
