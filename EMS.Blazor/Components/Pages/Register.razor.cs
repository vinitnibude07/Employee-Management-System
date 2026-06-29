using EMS.Blazor.Models;
using EMS.Shared.DTOs;
using Microsoft.JSInterop;

namespace EMS.Blazor.Components.Pages
{
    public partial class Register
    {
        private double? latitude;

        private double? longitude;

        private string? locationMessage;
        private RegisterModel registerModel = new();

        private bool sameAddress
        {
            get => _sameAddress;
            set
            {
                _sameAddress = value;

                if (_sameAddress)
                {
                    registerModel.CurrentAddress =
                        registerModel.PermanentAddress;
                }
            }
        }

        private bool _sameAddress;

        private async Task RegisterUser()
        {
            var request = new RegisterRequestDto
            {
                FirstName = registerModel.FirstName,
                MiddleName = registerModel.MiddleName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber,
                PermanentAddress = registerModel.PermanentAddress,
                CurrentAddress = registerModel.CurrentAddress,
                UserName = registerModel.UserName,
                Password = registerModel.Password,
                ConfirmPassword = registerModel.ConfirmPassword
            };

            var result = await AuthService.RegisterAsync(request);

            if (result)
            {
                Navigation.NavigateTo("/login");
            }
        }

        private void BackToLogin()
        {
            Navigation.NavigateTo("/login");
        }

        private async Task GetLocation()
        {
            try
            {
                var location = await JS.InvokeAsync<LocationModel>(
                    "getCurrentLocation");

                latitude = location.Latitude;
                longitude = location.Longitude;

                locationMessage =
                    $"Location captured successfully.";
            }
            catch (Exception ex)
            {
                locationMessage = ex.Message;
            }
        }
    }
}
