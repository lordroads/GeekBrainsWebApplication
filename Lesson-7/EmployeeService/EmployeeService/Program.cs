using EmployeeService.Database.Data;
using EmployeeService.Models.Dto;
using EmployeeService.Models.Requests;
using EmployeeService.Models.Validators;
using EmployeeService.Services;
using EmployeeService.Services.Implementations;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5001);
});

// Add services to the container.
#region Configure GRPc

builder.Services.AddGrpc();


#endregion

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

#region Configure EF (EmployeeDatabase DataBase)

builder.Services.AddDbContext<EmployeeServiceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["Settings:DatabaseOptions:ConnectionString"]);
});

#endregion

#region Configure Repository

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

#region Configure Authenticate

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticateService.SecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();

#endregion

#region Configure FluentValidator

builder.Services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
builder.Services.AddScoped<IValidator<DepartmentDto>, DepartmentDtoValidator>();
builder.Services.AddScoped<IValidator<EmployeeDto>, EmployeeDtoValidator>();
builder.Services.AddScoped<IValidator<EmployeeTypeDto>, EmployeeTypeDtoValidator>();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Сервис по регистрации сотрудников", Version = "v1"});

    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer schema (Example: 'Bearer 123213dfdslnfsdknf')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpLogging(); не работает с GRPc!!!!

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseWhen(ctx => ctx.Request.ContentType != "application/grpc",
    builder =>
    {
        builder.UseHttpLogging();
    });

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DictionariesService>();
});

app.Run();
