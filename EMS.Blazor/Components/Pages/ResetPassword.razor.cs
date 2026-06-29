using EMS.Shared.DTOs;

namespace EMS.Blazor.Components.Pages
{
    public partial class ResetPassword
    {
        private ResetPasswordRequestDto model = new();

        private string? message;

        private async Task SubmitResetPassword()
        {
            var response = await Http.PostAsJsonAsync(
                "https://localhost:7106/api/Auth/reset-password",
                model);

            if (response.IsSuccessStatusCode)
            {
                message = "Password reset successfully.";
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
