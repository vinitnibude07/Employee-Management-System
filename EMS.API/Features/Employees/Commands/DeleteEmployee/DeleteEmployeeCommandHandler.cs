using EMS.API.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler
    : IRequestHandler<DeleteEmployeeCommand, string>
{
    private readonly AppDbContext _context;

    public DeleteEmployeeCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(
        DeleteEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(
                e => e.Id == request.Id,
                cancellationToken);

        if (employee == null)
            return "Employee not found.";

        _context.Employees.Remove(employee);

        await _context.SaveChangesAsync(cancellationToken);

        return "Employee deleted successfully.";
    }
}