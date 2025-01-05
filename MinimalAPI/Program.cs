using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Validator;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblies([typeof(EmployeeValidator).Assembly]);

var app = builder.Build();

app.MapGet("/employee", () =>
{
    return InMemoryRepository.InMemoryEmployeeRepository;
});

app.MapPost("/employee", async (Employee employee, IValidator<Employee> validator) =>
{
    var validationResult = await validator.ValidateAsync(employee);
    if (!validationResult.IsValid)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Title = "Validation Failed",
            Status = StatusCodes.Status400BadRequest,
            Detail = "One or more validation errors occured!",
            Instance = "/employee",
            Errors = validationResult.ToDictionary()
        };
        return Results.BadRequest(problemDetails);
    }

    if (InMemoryRepository.InMemoryEmployeeRepository.Any(e => e.Id == employee.Id))
        return Results.BadRequest($"Employee with ID {employee.Id} already exists.");

    InMemoryRepository.InMemoryEmployeeRepository.Add(employee);
    return TypedResults.Created($"/todoitems/{employee.Id}", employee);
});

app.Run();
