using System.ComponentModel.DataAnnotations;

namespace EMS.Blazor.Models;

public class EmployeeModel
{
    [Required]
    public string EmployeeName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    [Required]
    public decimal Salary { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[0-9]{10}$",
    ErrorMessage = "Phone Number must be exactly 10 digits and contain only numeric values.")]
    public string PhoneNumber { get; set; } = string.Empty;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    [Required(ErrorMessage = "Please select a department.")]
    public int DepartmentId { get; set; }
}