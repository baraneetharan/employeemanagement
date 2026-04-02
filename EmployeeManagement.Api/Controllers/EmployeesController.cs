using MediatR;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using EmployeeManagement.Application.Features.Employees.Commands;
using EmployeeManagement.Application.Features.Employees.Queries;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmployeeCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _mediator.Send(new GetAllEmployeesQuery()));
}