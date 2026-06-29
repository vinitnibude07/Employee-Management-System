namespace EMS.Shared.DTOs
{
    public class EmployeeResponseDto
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}