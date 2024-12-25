using Domain;
using FluentValidation;
using FluentValidation.AspNetCore;
using Validator;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblies([typeof(EmployeeValidator).Assembly]);

var app = builder.Build();

app.MapGet("/employee", () =>
{
    return InMemoryRepository.InMemoryEmployeeRepository;
});

app.MapPost("/employee", (Employee employee) =>
{
    if (InMemoryRepository.InMemoryEmployeeRepository.Any(e => e.Id == employee.Id))
        return Results.BadRequest($"Employee with ID {employee.Id} already exists.");

    InMemoryRepository.InMemoryEmployeeRepository.Add(employee);
    return TypedResults.Created($"/todoitems/{employee.Id}", employee);
});

app.Run();
