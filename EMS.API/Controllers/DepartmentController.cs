using EMS.API.Features.Departments.Queries.GetDepartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _mediator.Send(new GetDepartmentsQuery());
        return Ok(departments);
    }
}