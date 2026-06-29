using EMS.API.Data;
using EMS.API.Features.Employees.Commands.UpdateEmployee;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, string>
    {
        private readonly AppDbContext _context;

        public UpdateEmployeeCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (request.DepartmentId <= 0)
            {
                throw new Exception($"DEBUG: Attempted to save with an invalid DepartmentId: {request.DepartmentId}");
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (employee == null) return "Employee not found.";

            // DEBUG: Check if the department ID is valid BEFORE saving
            var deptExists = await _context.Departments.AnyAsync(d => d.Id == request.DepartmentId, cancellationToken);

            if (!deptExists)
            {
                // This stops the crash and tells you exactly what went wrong
                return $"Error: Department ID {request.DepartmentId} does not exist in the database!";
            }

            // Mapping
            employee.EmployeeName = request.EmployeeName;
            employee.Description = request.Description;
            employee.Salary = request.Salary;
            employee.Email = request.Email;
            employee.PhoneNumber = request.PhoneNumber;
            employee.IsActive = request.IsActive;
            employee.Latitude = request.Latitude;
            employee.Longitude = request.Longitude;
            employee.DepartmentId = request.DepartmentId;

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return "Employee updated successfully.";
        }
    }
}