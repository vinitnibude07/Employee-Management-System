using System.ComponentModel.DataAnnotations;

namespace EMS.Blazor.Models;

public class RegisterModel
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
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
    [MinLength(5, ErrorMessage = "Username must contain at least 5 characters.")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [RegularExpression(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must contain 8 characters, 1 capital letter, 1 number and 1 special character.")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare("Password",
        ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}