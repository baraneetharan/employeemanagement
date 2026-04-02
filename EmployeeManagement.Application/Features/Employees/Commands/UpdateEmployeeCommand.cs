using MediatR;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Features.Employees.DTOs;

namespace EmployeeManagement.Application.Features.Employees.Commands;

public record UpdateEmployeeCommand(
    int Id,
    string FirstName,
    string LastName,
    decimal Salary
) : IRequest<bool>;

public class UpdateEmployeeCommandHandler 
    : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEmployeeCommandHandler(IEmployeeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken ct)
    {
        var employee = await _repository.GetByIdAsync(request.Id, ct);
        if (employee == null) return false;

        employee.UpdateDetails(request.FirstName, request.LastName, request.Salary);

        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}