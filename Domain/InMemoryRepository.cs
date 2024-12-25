namespace Domain;
public static class InMemoryRepository
{
    public static readonly List<Employee> InMemoryEmployeeRepository =
    [
        new Employee(1, "John", "IT", "Software Engineer"),
        new Employee(2, "Jane", "HR", "HR Manager"),
        new Employee(3, "Doe", "Finance", "Finance Manager")
    ];
}
