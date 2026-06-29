using MediatR;

namespace EMS.API.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest<string>
{
    public int Id { get; set; }

    public string? EmployeeName { get; set; }

    public string? Description { get; set; }

    public decimal Salary { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int DepartmentId { get; set; }
}