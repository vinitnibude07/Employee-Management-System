using System.ComponentModel.DataAnnotations;

namespace EMS.Shared.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Username must contain at least 5 characters.")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}