// Application/Features/Employees/Commands/CreateEmployeeCommand.cs
using MediatR;
using EmployeeManagement.Application.Features.Employees.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Application.Interfaces;

namespace EmployeeManagement.Application.Features.Employees.Commands;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    decimal Salary
) : IRequest<int>;

public class CreateEmployeeCommandHandler 
    : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IEmployeeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmployeeCommandHandler(IEmployeeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = Employee.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.DateOfBirth,
            request.Salary);

        await _repository.AddAsync(employee);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}