using System.Data;
using EmployeeManagement.Application.Features.Employees.DTOs;
using MediatR;
using Dapper;

namespace EmployeeManagement.Application.Features.Employees.Queries;

public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto?>;

public class GetEmployeeByIdQueryHandler 
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IDbConnection _connection;

    public GetEmployeeByIdQueryHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken ct)
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
            WHERE id = @Id;
        """;

        return await _connection.QueryFirstOrDefaultAsync<EmployeeDto>(sql, new { request.Id });
    }
}