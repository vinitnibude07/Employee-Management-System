using EMS.Shared.DTOs;

namespace EMS.Blazor.Components.Pages
{
    public partial class ForgotPassword
    {
        private ForgotPasswordRequestDto model = new();

        private ResetPasswordRequestDto resetModel = new();

        private bool otpSent = false;

        private string? message;

        private async Task SendOtp()
        {
            var response = await Http.PostAsJsonAsync(
                "https://localhost:7106/api/Auth/forgot-password",
                model);

            if (response.IsSuccessStatusCode)
            {
                otpSent = true;

                resetModel.Email = model.Email;

                message = "OTP sent successfully. Please check your email.";
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }
        }

        private async Task ResetUserPassword()
        {
            var response = await Http.PostAsJsonAsync(
                "https://localhost:7106/api/Auth/reset-password",
                resetModel);

            if (response.IsSuccessStatusCode)
            {
                message = "Password reset successfully.";

                await Task.Delay(2000);

                Navigation.NavigateTo("/login");
            }
            else
            {
                message = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
