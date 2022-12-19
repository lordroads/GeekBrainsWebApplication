using EmployeeService.Database.Data;
using EmployeeService.Models.Dto;
using EmployeeServiceProto;
using FluentValidation;
using FluentValidation.Results;
using Grpc.Core;
using static EmployeeServiceProto.DictionariesService;

namespace EmployeeService.Services.Implementations;

public class DictionariesService : DictionariesServiceBase
{
    private readonly IEmployeeTypeRepository _employeeTypeRepository;
    private readonly IValidator<EmployeeTypeDto> _validator;

    public DictionariesService(IEmployeeTypeRepository employeeTypeRepository, IValidator<EmployeeTypeDto> validator)
    {
        _employeeTypeRepository = employeeTypeRepository;
        _validator = validator;
    }

    public override Task<CreateEmployeeTypeResponse> Create(CreateEmployeeTypeRequest request, ServerCallContext context)
    {
        EmployeeTypeDto employeeTypeDto = new EmployeeTypeDto
        {
            Description = request.Description
        };

        ValidationResult validationResult = _validator.Validate(employeeTypeDto);

        if (!validationResult.IsValid)
        {
            return Task.FromResult(new CreateEmployeeTypeResponse());
        }

        var id = _employeeTypeRepository.Create(new EmployeeType
        {
            Id = employeeTypeDto.Id,
            Description = employeeTypeDto.Description
        });

        return Task.FromResult(new CreateEmployeeTypeResponse { Id = id });
    }

    public override Task<GetAllEmployeeTypeResponse> GetAll(GetAllEmployeeTypeRequest request, ServerCallContext context)
    {
        GetAllEmployeeTypeResponse response = new GetAllEmployeeTypeResponse();
        response.EmployeeTypes.AddRange(
            _employeeTypeRepository.GetAll().Select(x => new EmployeeServiceProto.GetAllEmployeeTypeResponse.Types.EmployeeTypeDto
            {
                Id = x.Id,
                Description = x.Description
            }).ToList()
            );

        return Task.FromResult(response);
    }

    public override Task<GetByIdEmployeeTypeResponse> GetById(GetByIdEmployeeTypeRequest request, ServerCallContext context)
    {
        EmployeeType employeeType = _employeeTypeRepository.GetById(request.Id);
        return Task.FromResult(new GetByIdEmployeeTypeResponse { Id = employeeType.Id, Description = employeeType.Description });
    }

    public override Task<UpdateEmployeeTypeResponse> Update(UpdateEmployeeTypeRequest request, ServerCallContext context)
    {
        bool result = _employeeTypeRepository.Update(new EmployeeType
        {
            Id = request.Id,
            Description = request.Description
        });


        return Task.FromResult(new UpdateEmployeeTypeResponse {  Result = result });
    }

    public override Task<DeleteEmployeeTypeResponse> Delete(DeleteEmployeeTypeRequest request, ServerCallContext context)
    {
        bool result = _employeeTypeRepository.Delete(request.Id);

        return Task.FromResult(new DeleteEmployeeTypeResponse {  Result = result });
    }
}
