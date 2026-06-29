using System.ComponentModel.DataAnnotations;

namespace EMS.Shared.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue,
        ErrorMessage = "Salary must be greater than zero.")]
        public decimal Salary { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[0-9]{10}$",
        ErrorMessage = "Phone Number must be exactly 10 digits and contain only numeric values.")] 
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}