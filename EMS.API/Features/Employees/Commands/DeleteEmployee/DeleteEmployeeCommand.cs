using MediatR;

namespace EMS.API.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<string>
{
    public int Id { get; set; }
}