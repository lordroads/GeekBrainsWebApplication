using Grpc.Net.Client;
using static EmployeeServiceProto.DictionariesService;

var channel = GrpcChannel.ForAddress("http://localhost:5001/");
DictionariesServiceClient client = new DictionariesServiceClient(channel);

Console.WriteLine("Введите тип сотрудника: ");

var response = client.Create(new EmployeeServiceProto.CreateEmployeeTypeRequest
{
    Description = Console.ReadLine()
});

if (response != null)
{
    Console.WriteLine($"ТИп сотрудника успешно добавлен: #{response.Id}");
}

var getAllEmployeeTypeResponse = client.GetAll(new EmployeeServiceProto.GetAllEmployeeTypeRequest());

foreach (var employeeType in getAllEmployeeTypeResponse.EmployeeTypes)
{
    Console.WriteLine($"#{employeeType.Id} / {employeeType.Description}");
}
