using EMS.API.Data;
using EMS.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Departments.Queries.GetDepartments;

// The MediatR Request definition
public record GetDepartmentsQuery : IRequest<List<DepartmentResponseDto>>;

// The actual Handler execution logic
public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<DepartmentResponseDto>>
{
    private readonly AppDbContext _context;

    public GetDepartmentsQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepartmentResponseDto>> Handle(
        GetDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Departments
            .Select(d => new DepartmentResponseDto
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToListAsync(cancellationToken);
    }
}