using EMS.Shared.DTOs;
using MediatR;

namespace EMS.API.Features.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQuery : IRequest<List<EmployeeResponseDto>>
{
}