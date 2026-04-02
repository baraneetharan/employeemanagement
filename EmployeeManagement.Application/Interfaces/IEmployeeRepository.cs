// EmployeeManagement.Application/Interfaces/IEmployeeRepository.cs
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Application.Features.Employees.DTOs;

namespace EmployeeManagement.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(int id, CancellationToken cancellationToken = default); // Soft delete
    
    // Read operations using Dapper (better performance for queries)
    Task<List<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<EmployeeDto?> GetByIdDtoAsync(int id, CancellationToken cancellationToken = default);
    
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
}