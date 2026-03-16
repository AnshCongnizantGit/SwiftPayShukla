using Microsoft.EntityFrameworkCore;
using SwiftPay.Configuration;
using AutoMapper;
using SwiftPay.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("SwiftPayDb")));

// Register repository and service
builder.Services.AddScoped<SwiftPay.Repositories.Interfaces.IRemittanceRepository, SwiftPay.Repositories.RemittanceRepository>();
builder.Services.AddScoped<SwiftPay.Services.Interfaces.IRemittanceService, SwiftPay.Services.RemittanceService>();

// AutoMapper registration - ensure AutoMapper and its extensions package versions are compatible.
// Register all profiles in the assembly so IMapper is available for services.
builder.Services.AddAutoMapper(typeof(RemittanceProfile));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
