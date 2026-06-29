using EMS.API.Data;
using EMS.Shared.Models;
using MediatR;

namespace EMS.API.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler
    : IRequestHandler<CreateEmployeeCommand, string>
{
    private readonly AppDbContext _context;

    public CreateEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            EmployeeName = request.EmployeeName,
            Description = request.Description,
            Salary = request.Salary,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            DepartmentId = request.DepartmentId, 
            IsActive = request.IsActive,
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        _context.Employees.Add(employee);

        await _context.SaveChangesAsync(cancellationToken);

        return "Employee added successfully.";
    }
}