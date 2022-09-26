using EmployeeService.Services;
using EmployeeService.Services.Implementations;
using Microsoft.AspNetCore.HttpLogging;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Configure Automapper

//TODO: В дальнейшем для использования AutoMapper'a, подключение MapperProfile и регистрирование mapper'a в контейнера как Singleton.

//var mapperConfiguration = new MapperConfiguration(mapper => mapper.AddProfile(new MapperProfile()));
//var mapper = mapperConfiguration.CreateMapper();
//builder.Services.AddSingleton(mapper);

#endregion

#region Configure Options

//TODO: Использование объекта данных для поключение к БД

//builder.Services.Configure<DatabaseOptions>(options =>
//{
//    builder.Configuration.GetSection("Settings:DatabaseOptions").Bind(options);
//});

#endregion

#region Configure DataBase

//TODO: Area for EntityFramework configurations.

#endregion

#region Configure Service

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

#endregion

#region Configure Logging

builder.Host.ConfigureLogging(logger =>
{
    logger.ClearProviders();
    logger.AddConsole();
}).UseNLog(new NLogAspNetCoreOptions { RemoveLoggerFactoryFilter = true });

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.ResponseHeaders.Add("Authorization");
    logging.ResponseHeaders.Add("X-Real-IP");
    logging.ResponseHeaders.Add("X-Forwarder-For");
});

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
