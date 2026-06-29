using System.ComponentModel.DataAnnotations;

namespace EMS.Shared.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[0-9]{10}$",
    ErrorMessage = "Phone Number must be exactly 10 digits and contain only numeric values.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string PermanentAddress { get; set; } = string.Empty;

        [Required]
        public string CurrentAddress { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public string Role { get; set; } = "Admin";

        public string? OtpCode { get; set; }

        public DateTime? OtpExpiry { get; set; }
        public string? ResetOtp { get; set; }

        public DateTime? ResetOtpExpiry { get; set; }

        public string? PasswordResetOtp { get; set; }

        public DateTime? PasswordResetOtpExpiry { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}