using Dapper;
using EmployeeManagement.Application.Features.Employees.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagement.Infrastructure.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    private readonly IDbConnection _connection;

    public EmployeeRepository(AppDbContext context, IDbConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .FirstOrDefaultAsync(e => e.Id == id && e.IsActive, cancellationToken);
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
    }

    public Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await GetByIdAsync(id, cancellationToken);
        if (employee is null)
        {
            return;
        }

        employee.Deactivate();
        _context.Employees.Update(employee);
    }

    public async Task<List<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                id AS Id,
                first_name AS FirstName,
                last_name AS LastName,
                email AS Email,
                date_of_birth AS DateOfBirth,
                salary AS Salary,
                is_active AS IsActive
            FROM employees
            WHERE is_active = true
            ORDER BY id DESC;
            """;

        var employees = await _connection.QueryAsync<EmployeeDto>(new CommandDefinition(sql, cancellationToken: cancellationToken));
        return employees.ToList();
    }

    public async Task<EmployeeDto?> GetByIdDtoAsync(int id, CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                id AS Id,
                first_name AS FirstName,
                last_name AS LastName,
                email AS Email,
                date_of_birth AS DateOfBirth,
                salary AS Salary,
                is_active AS IsActive
            FROM employees
            WHERE id = @Id AND is_active = true;
            """;

        return await _connection.QueryFirstOrDefaultAsync<EmployeeDto>(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT EXISTS(SELECT 1 FROM employees WHERE email = @Email AND is_active = true);";

        return await _connection.ExecuteScalarAsync<bool>(
            new CommandDefinition(sql, new { Email = email }, cancellationToken: cancellationToken));
    }
}
