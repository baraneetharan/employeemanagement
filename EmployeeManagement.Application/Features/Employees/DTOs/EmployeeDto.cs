// EmployeeManagement.Application/Features/Employees/DTOs/EmployeeDto.cs
namespace EmployeeManagement.Application.Features.Employees.DTOs;

public record EmployeeDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    decimal Salary,
    bool IsActive
);