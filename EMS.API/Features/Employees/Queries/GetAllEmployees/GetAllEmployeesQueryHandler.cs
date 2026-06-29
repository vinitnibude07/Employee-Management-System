using EMS.API.Data;
using EMS.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Employees.Queries.GetAllEmployees;

public class GetAllEmployeesQueryHandler
    : IRequestHandler<GetAllEmployeesQuery, List<EmployeeResponseDto>>
{
    private readonly AppDbContext _context;

    public GetAllEmployeesQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeResponseDto>> Handle(
        GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .OrderBy(e => e.Id)
            .Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                EmployeeName = e.EmployeeName,
                Description = e.Description,
                Salary = e.Salary,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                DepartmentId = e.DepartmentId,         
                DepartmentName = e.Department.Name,     
                IsActive = e.IsActive,
                CreatedDate = e.CreatedDate,
                Latitude = e.Latitude,
                Longitude = e.Longitude
            })
            .ToListAsync(cancellationToken);
    }
}