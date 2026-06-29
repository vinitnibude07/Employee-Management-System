using System.ComponentModel.DataAnnotations;

namespace EMS.Shared.DTOs;

public class ResetPasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Otp { get; set; } = string.Empty;

    [Required]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must contain at least 8 characters, 1 uppercase letter, 1 number, and 1 special character.")]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [Compare("NewPassword",
        ErrorMessage = "Confirm Password must match New Password.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}