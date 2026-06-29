using System.ComponentModel.DataAnnotations;

namespace EMS.Blazor.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(5, ErrorMessage = "Username must contain at least 5 characters.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must contain at least 8 characters.")]
    public string Password { get; set; } = string.Empty;
}