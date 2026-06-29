using EMS.Shared.DTOs;
using MediatR;

namespace EMS.API.Features.Employees.Queries.GetPagedEmployees;

public class GetPagedEmployeesQuery
    : IRequest<PagedResultDto<EmployeeResponseDto>>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 5;

    public string SortBy { get; set; } = "id";

    public string SearchText { get; set; } = string.Empty;
}