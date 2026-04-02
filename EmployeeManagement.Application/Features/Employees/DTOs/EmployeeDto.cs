// EmployeeManagement.Application/Features/Employees/DTOs/EmployeeDto.cs
using System;

namespace EmployeeManagement.Application.Features.Employees.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }

    public EmployeeDto()
    {
    }
}