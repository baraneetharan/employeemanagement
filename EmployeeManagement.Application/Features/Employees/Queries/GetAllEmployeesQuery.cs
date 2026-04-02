// Application/Features/Employees/Queries/GetAllEmployeesQuery.cs
using EmployeeManagement.Application.Features.Employees.DTOs;
using Dapper;
using MediatR;
using System.Data;

namespace EmployeeManagement.Application.Features.Employees.Queries;

public record GetAllEmployeesQuery : IRequest<List<EmployeeDto>>;

public class GetAllEmployeesQueryHandler 
    : IRequestHandler<GetAllEmployeesQuery, List<EmployeeDto>>
{
    private readonly IDbConnection _connection; // Dapper

    public GetAllEmployeesQueryHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<EmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
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

        var employees = await _connection.QueryAsync<EmployeeDto>(sql);
        return employees.ToList();
    }
}