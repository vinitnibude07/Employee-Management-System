using EMS.Blazor.Models;
using EMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EMS.Blazor.Components.Pages
{
    public partial class EditEmployee
    {
        [Parameter] public int Id { get; set; }

        [Inject] private EMS.Blazor.Services.EmployeeService EmployeeService { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IJSRuntime JS { get; set; } = default!;

        private EmployeeResponseDto? employee;
        private EmployeeUpdateDto updateModel = new();
        private List<DepartmentResponseDto> departments = new();
        private string? locationMessage;
        private bool isSaving;
        private bool showSuccessPopup;

        protected override async Task OnParametersSetAsync()
        {
            // 1. Fire off both tasks simultaneously
            var deptTask = EmployeeService.GetDepartmentsAsync();
            var empTask = EmployeeService.GetEmployeeByIdAsync(Id);

            // 2. Wait until BOTH are fully finished loading
            await Task.WhenAll(deptTask, empTask);

            // 3. Safely extract results into memory
            departments = deptTask.Result ?? new();
            employee = empTask.Result;

            // 👇 ADD THIS TEMPORARY DEBUG BLOCK HERE 👇
            if (employee != null)
            {
                System.Console.WriteLine($"--- DEBUGGING DROPDOWN BINDING ---");
                System.Console.WriteLine($"Employee Name: {employee.EmployeeName}");
                System.Console.WriteLine($"DepartmentId Value from API: {employee.DepartmentId}");
                System.Console.WriteLine($"Total Departments Loaded: {departments.Count}");
                foreach (var d in departments)
                {
                    System.Console.WriteLine($"Available Dept in Dropdown: ID = {d.Id}, Name = {d.Name}");
                }
                System.Console.WriteLine($"-----------------------------------");
            }

            if (employee != null)
            {
                // 4. Map properties over to the bound UI form model
                updateModel.Id = employee.Id;
                updateModel.EmployeeName = employee.EmployeeName;
                updateModel.Description = employee.Description;
                updateModel.Salary = employee.Salary;
                updateModel.Email = employee.Email;
                updateModel.PhoneNumber = employee.PhoneNumber;
                updateModel.IsActive = employee.IsActive;
                updateModel.Latitude = employee.Latitude;
                updateModel.Longitude = employee.Longitude;

                // 5. CRITICAL: Assign DepartmentId LAST after the options loop is populated
                updateModel.DepartmentId = employee.DepartmentId;

                // 6. Force Blazor to rebuild the UI with options ready and matching ID selected
                StateHasChanged();
            }
        }

        private async Task UpdateEmployee()
        {
            if (isSaving) return;

            isSaving = true;

            await Task.Delay(1500);

            var request = new EmployeeUpdateDto
            {
                Id = updateModel.Id,
                EmployeeName = updateModel.EmployeeName,
                Description = updateModel.Description,
                Salary = updateModel.Salary,
                Email = updateModel.Email,
                PhoneNumber = updateModel.PhoneNumber,
                DepartmentId = updateModel.DepartmentId,
                IsActive = updateModel.IsActive,
                Latitude = updateModel.Latitude,
                Longitude = updateModel.Longitude
            };

            var success = await EmployeeService.UpdateEmployeeAsync(request);

            if (success)
            {
                isSaving = false;
                showSuccessPopup = true;

                // 🛠️ FORCE RE-RENDER OVERRIDE
                StateHasChanged();
            }
            else
            {
                isSaving = false;
                StateHasChanged(); // Force re-render to unlock buttons if it failed
            }
        }

        private void ClosePopupAndNavigate()
        {
            showSuccessPopup = false;
            Navigation.NavigateTo("/employeelist");
        }

        private async Task GetLocation()
        {
            try
            {
                var location = await JS.InvokeAsync<LocationModel>("getCurrentLocation");
                updateModel.Latitude = location.Latitude;
                updateModel.Longitude = location.Longitude;
                locationMessage = $"Location captured. Lat: {location.Latitude}, Lng: {location.Longitude}";
            }
            catch (Exception ex)
            {
                locationMessage = ex.Message;
            }
        }

        private void Cancel()
        {
            Navigation.NavigateTo("/employeelist");
        }
    }
}