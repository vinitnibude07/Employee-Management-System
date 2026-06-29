using EMS.API.Data;
using EMS.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeResponseDto?>
{
    private readonly AppDbContext _context;

    public GetEmployeeByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeResponseDto?> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Where(e => e.Id == request.Id)
            .Select(e => new EmployeeResponseDto
            {
                Id = e.Id,
                EmployeeName = e.EmployeeName,
                Description = e.Description,
                Salary = e.Salary,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                IsActive = e.IsActive,
                CreatedDate = e.CreatedDate,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                DepartmentId = e.DepartmentId
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}