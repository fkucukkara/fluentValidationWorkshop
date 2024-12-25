using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetEmployees()
    {
        return Ok(InMemoryRepository.InMemoryEmployeeRepository);
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
        var employee = InMemoryRepository.InMemoryEmployeeRepository.FirstOrDefault(e => e.Id == id);
        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        return Ok(employee);
    }

    [HttpPost]
    public IActionResult AddEmployee(Employee employee, IValidator<Employee> validator)
    {
        // Close that line in Program.cs: builder.Services.AddFluentValidationAutoValidation();

        var validationResult = validator.Validate(employee);
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

            return new ObjectResult(problemDetails);
        }

        if (InMemoryRepository.InMemoryEmployeeRepository.Any(e => e.Id == employee.Id))
            return BadRequest($"Employee with ID {employee.Id} already exists.");

        InMemoryRepository.InMemoryEmployeeRepository.Add(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
    }


    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, Employee updatedEmployee)
    {
        // Auto validation is expected!
        // Open that line in Program.cs: builder.Services.AddFluentValidationAutoValidation();

        var employeeIndex = InMemoryRepository.InMemoryEmployeeRepository.FindIndex(e => e.Id == id);
        if (employeeIndex == -1)
            return NotFound($"Employee with ID {id} not found.");

        InMemoryRepository.InMemoryEmployeeRepository[employeeIndex] = updatedEmployee;
        return Ok(updatedEmployee);
    }

    // Delete an employee
    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var employee = InMemoryRepository.InMemoryEmployeeRepository.FirstOrDefault(e => e.Id == id);
        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        InMemoryRepository.InMemoryEmployeeRepository.Remove(employee);
        return NoContent();
    }
}