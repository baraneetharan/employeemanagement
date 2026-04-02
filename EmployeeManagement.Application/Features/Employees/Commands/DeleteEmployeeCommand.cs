using MediatR;
using EmployeeManagement.Application.Interfaces;

namespace EmployeeManagement.Application.Features.Employees.Commands;

public record DeleteEmployeeCommand(int Id) : IRequest<bool>;

public class DeleteEmployeeCommandHandler 
    : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEmployeeCommandHandler(IEmployeeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken ct)
    {
        var employee = await _repository.GetByIdAsync(request.Id, ct);
        if (employee == null) return false;

        employee.Deactivate();

        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}
