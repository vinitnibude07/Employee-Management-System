using EMS.Shared.DTOs;
using MediatR;

namespace EMS.API.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQuery : IRequest<EmployeeResponseDto?>
{
    public int Id { get; set; }
}