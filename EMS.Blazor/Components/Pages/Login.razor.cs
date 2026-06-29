using EMS.Blazor.Models;
using EMS.Shared.DTOs;
using System;
using System.Threading.Tasks;

namespace EMS.Blazor.Components.Pages
{
    public partial class Login
    {
        private LoginModel loginModel = new();
        private string errorMessage = string.Empty;
        private bool isLoggingIn = false;

        private async Task LoginUser()
        {
            errorMessage = string.Empty;
            isLoggingIn = true;

            try
            {
                var request = new LoginRequestDto
                {
                    UserName = loginModel.UserName,
                    Password = loginModel.Password
                };

                var response = await AuthService.LoginAsync(request);

                Console.WriteLine($"RESPONSE IS NULL: {response == null}");
                if (response is not null)
                {
                    // 1. Store the security validation token string
                    TokenService.SetToken(response.Token);

                    // 2. FIX: Capture and store the typed username string so the dashboard displays it properly
                    TokenService.SetUsername(loginModel.UserName);

                    // 3. Redirect directly back to the clean root workspace layout
                    Navigation.NavigateTo("/");
                }
                else
                {
                    errorMessage = "Invalid username or password.";
                    Console.WriteLine("LOGIN FAILED");
                    isLoggingIn = false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An unexpected error occurred during sign-in.";
                Console.WriteLine($"Error: {ex.Message}");
                isLoggingIn = false;
            }
        }
    }
}