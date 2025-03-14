using HGKATA.Core.Domain;
using HGKATA.Core.Services;
using HGKATA.Infrastructure;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register the interface and its implementation
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//builder.Services.AddScoped<ICustomerService, CustomerService>();

// Add services to the container.
builder.Services.AddControllers(
    options => options.InputFormatters.Add(new PlainTextFormatter())
);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Honest Greens Customers Classification API", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();

app.Run();
