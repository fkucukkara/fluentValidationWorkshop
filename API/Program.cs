using FluentValidation;
using FluentValidation.AspNetCore;
using Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// individual registration
//builder.Services.AddScoped<IValidator<Employee>, EmployeeValidator>();

// assembly registration

// Optionally, you can auto validation which is sampled in PUT endpoint
//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblies([typeof(EmployeeValidator).Assembly], includeInternalTypes: true);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
