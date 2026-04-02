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
            SELECT id, first_name, last_name, email, date_of_birth, salary, is_active
            FROM employees 
            WHERE id = @Id AND is_active = true;
        """;

        return await _connection.QueryFirstOrDefaultAsync<EmployeeDto>(sql, new { request.Id });
    }
}