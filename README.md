# Fluent Validation in ASP.NET Core APIs

This repository demonstrates the usage of Fluent Validation in both Controller-based APIs and Minimal APIs. The examples cover core validation scenarios, including manual validation and automatic validation. Key differences between the two approaches are highlighted.

## Overview

Fluent Validation is a popular library for defining complex validation rules for .NET objects in a fluent, expressive way. It integrates seamlessly with ASP.NET Core, allowing developers to create cleaner and more maintainable validation logic.

This repository covers:
- How to implement Fluent Validation for Controller-based APIs.
- How to implement Fluent Validation for Minimal APIs.
- Auto-validation support in Controller-based APIs.
- The use of `ValidationProblemDetails` for consistent error handling.

## Validation

Validation rules are defined using custom validator classes that inherit from `AbstractValidator<T>`. These rules can be applied manually or automatically:

- **Controller-based API**: Leverages ModelState for binding validation results automatically.
- **Minimal API**: Requires explicit invocation of the validation logic, as there is no ModelState equivalent.

Example validation rule:
```csharp
public class ExampleModelValidator : AbstractValidator<ExampleModel>
{
    public ExampleModelValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Age).InclusiveBetween(18, 60).WithMessage("Age must be between 18 and 60.");
    }
}
```

## Auto Validation

Automatic validation is available in Controller-based APIs and is handled by ASP.NET Core when Fluent Validation is properly integrated. This involves:

1. Adding Fluent Validation to the service collection:
   ```csharp
   services.AddFluentValidationAutoValidation();
   services.AddValidatorsFromAssemblies([typeof(SampleValidator).Assembly]);
   ```

2. Errors being added to `ModelState` automatically.

In Minimal APIs, there is no `ModelState`, so auto-validation is not applicable. Instead, you need to manually call the validator:

```csharp
var validator = new ExampleModelValidator();
var result = validator.Validate(exampleModel);

if (!result.IsValid)
{
    return Results.ValidationProblem(result.ToDictionary());
}
```

## License
[![MIT License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

This project is licensed under the MIT License, which allows you to freely use, modify, and distribute the code. See the [`LICENSE`](LICENSE) file for full details.
