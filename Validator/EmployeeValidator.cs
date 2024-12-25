using Domain;
using FluentValidation;

namespace Validator;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.Department).NotEmpty();
        RuleFor(e => e.Designation).NotEmpty();
    }
}
