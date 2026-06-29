using EMS.Blazor.Models;
using EMS.Shared.DTOs;
using Microsoft.JSInterop;

namespace EMS.Blazor.Components.Pages
{
    public partial class AddEmployee
    {
        private string? locationMessage;
        private EmployeeModel employee = new();
        private List<DepartmentResponseDto> departments = new();

        // UX Processing States
        private bool isSaving;
        private bool showSuccessPopup;

        protected override async Task OnInitializedAsync()
        {
            departments = await EmployeeService.GetDepartmentsAsync() ?? new();
        }

        private async Task SaveEmployee()
        {
            if (isSaving) return;

            // 1. Check if the department selection is missing
            if (employee.DepartmentId == 0)
            {
                locationMessage = "❌ Frontend Guard: Please select a corporate department before saving.";
                return;
            }

            isSaving = true;
            locationMessage = "⏳ Processing registration..."; // Reset message status

            await Task.Delay(1500);

            var request = new EmployeeCreateDto
            {
                EmployeeName = employee.EmployeeName,
                Description = employee.Description,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DepartmentId = employee.DepartmentId,
                IsActive = employee.IsActive,
                Latitude = employee.Latitude,
                Longitude = employee.Longitude
            };

            var success = await EmployeeService.AddEmployeeAsync(request);

            if (success)
            {
                isSaving = false;
                locationMessage = null; // Clear messages on success
                showSuccessPopup = true;
            }
            else
            {
                isSaving = false;
                // 2. Expose the API rejection right on the UI screen
                locationMessage = "❌ API Error: The server rejected this employee. Check if the Email is a duplicate or if fields failed API validations.";
            }
        }

        private void ClosePopupAndNavigate()
        {
            showSuccessPopup = false;
            Navigation.NavigateTo("/employeelist");
        }

        private async Task GetLocation()
        {
            if (isSaving) return;

            try
            {
                var location = await JS.InvokeAsync<LocationModel>("getCurrentLocation");
                employee.Latitude = location.Latitude;
                employee.Longitude = location.Longitude;
                locationMessage = $"Location captured successfully. Lat: {location.Latitude}, Lng: {location.Longitude}";
            }
            catch (Exception ex)
            {
                locationMessage = ex.Message;
            }
        }
    }
}