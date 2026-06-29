using System.ComponentModel.DataAnnotations;

namespace EMS.Shared.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$",
        ErrorMessage = "Phone number must contain exactly 10 digits.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string PermanentAddress { get; set; } = string.Empty;

        [Required]
        public string CurrentAddress { get; set; } = string.Empty;

        [Required]
        [MinLength(5,
        ErrorMessage = "Username must contain at least 5 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain at least 1 capital letter, 1 number and 1 special character.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}