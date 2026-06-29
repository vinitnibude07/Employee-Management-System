using EMS.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Blazor.Components.Pages
{
    public partial class EmployeeList
    {
        private string errorMessage = string.Empty;
        private string statusFilter = "All";
        private string sortBy = "id";
        private string sortDirection = "";
        private string searchText = string.Empty;

        // Core UI State Trackers
        private bool isLoading = true;
        private bool isUnauthorized = false;

        private List<EmployeeResponseDto>? employees = null;
        private List<EmployeeResponseDto> filteredEmployees = new();
        private int currentPage = 1;

        private void AddEmployee()
        {
            Navigation.NavigateTo("/addemployee");
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadEmployees();
        }

        private void EditEmployee(int id)
        {
            Navigation.NavigateTo($"/editemployee/{id}");
        }

        private async Task DeleteEmployee(int id)
        {
            var confirmed = await JS.InvokeAsync<bool>(
                "confirm",
                "Are you sure you want to delete this employee?");

            if (!confirmed)
                return;

            var success = await EmployeeService.DeleteEmployeeAsync(id);

            if (success)
            {
                await LoadEmployees();
            }
        }

        private async Task LoadEmployees()
        {
            isLoading = true;
            isUnauthorized = false;

            var result = await EmployeeService
                .GetEmployeesAsync(
                    currentPage,
                    "id",
                    searchText);

            if (result != null)
            {
                isUnauthorized = false;
                employees = result.Items ?? new List<EmployeeResponseDto>();

                if (sortDirection != "")
                {
                    switch (sortBy)
                    {
                        case "id":
                            employees = sortDirection == "asc"
                                ? employees.OrderBy(e => e.Id).ToList()
                                : employees.OrderByDescending(e => e.Id).ToList();
                            break;
                        case "name":
                            employees = sortDirection == "asc"
                                ? employees.OrderBy(e => e.EmployeeName).ToList()
                                : employees.OrderByDescending(e => e.EmployeeName).ToList();
                            break;
                        case "description":
                            employees = sortDirection == "asc"
                                ? employees.OrderBy(e => e.Description).ToList()
                                : employees.OrderByDescending(e => e.Description).ToList();
                            break;
                        case "department":
                            employees = sortDirection == "asc"
                                ? employees.OrderBy(e => e.DepartmentName).ToList()
                                : employees.OrderByDescending(e => e.DepartmentName).ToList();
                            break;
                        case "status":
                            employees = sortDirection == "asc"
                                ? employees.OrderBy(e => e.IsActive).ToList()
                                : employees.OrderByDescending(e => e.IsActive).ToList();
                            break;
                        case "salary":
                            employees = sortDirection == "asc"
                                ? employees.OrderBy(e => e.Salary).ToList()
                                : employees.OrderByDescending(e => e.Salary).ToList();
                            break;
                        case "email":
                            employees = sortDirection == "asc"
                                ? employees.OrderBy(e => e.Email).ToList()
                                : employees.OrderByDescending(e => e.Email).ToList();
                            break;
                        case "phone":
                            employees = sortDirection == "asc"
                                ? employees.OrderBy(e => e.PhoneNumber).ToList()
                                : employees.OrderByDescending(e => e.PhoneNumber).ToList();
                            break;
                    }
                }

                ApplyClientFilters();
            }
            else
            {
                isUnauthorized = true;
                employees = new List<EmployeeResponseDto>();
                filteredEmployees = new List<EmployeeResponseDto>();
            }

            isLoading = false;
        }

        private void ApplyClientFilters()
        {
            if (employees == null) return;

            filteredEmployees = employees
                .Where(e =>
                    (string.IsNullOrWhiteSpace(searchText) ||
                    (e.EmployeeName != null && e.EmployeeName.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
                    &&
                    (statusFilter == "All"
                    || (statusFilter == "Active" && e.IsActive)
                    || (statusFilter == "Inactive" && !e.IsActive))
                ).ToList();
        }

        private async Task NextPage()
        {
            currentPage++;
            await LoadEmployees();
        }

        private async Task PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                await LoadEmployees();
            }
        }

        private async Task SearchEmployees()
        {
            currentPage = 1;
            await LoadEmployees();
        }

        private async Task ToggleSort(string column)
        {
            if (sortBy != column)
            {
                sortBy = column;
                sortDirection = "asc";
            }
            else if (sortDirection == "asc")
            {
                sortDirection = "desc";
            }
            else if (sortDirection == "desc")
            {
                sortDirection = "";
            }
            else
            {
                sortDirection = "asc";
            }

            await LoadEmployees();
            StateHasChanged();
        }

        private string GetSortIcon(string column)
        {
            if (sortBy != column || string.IsNullOrEmpty(sortDirection))
            {
                return "↕";
            }
            if (sortDirection == "asc")
            {
                return "▲";
            }
            return "▼";
        }

        private void FilterEmployees(ChangeEventArgs e)
        {
            statusFilter = e.Value?.ToString() ?? "All";
            ApplyClientFilters();
            StateHasChanged();
        }

        // FIXED: Changed to async Task and used JS Interop window.open to target a fresh tab
        private async Task OpenLocation(double latitude, double longitude)
        {
            var url = $"https://www.google.com/maps?q={latitude},{longitude}";
            await JS.InvokeVoidAsync("window.open", url, "_blank");
        }

        private async Task Logout()
        {
            try
            {
                await JS.InvokeVoidAsync("localStorage.removeItem", "authToken");
                await JS.InvokeVoidAsync("sessionStorage.clear");
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("console.error", $"Logout error: {ex.Message}");
            }
            finally
            {
                Navigation.NavigateTo("/", forceLoad: true);
            }
        }

        private async Task DownloadEmployeePdf()
        {
            try
            {
                var fileBytes = await EmployeeService.DownloadEmployeePdfAsync();

                if (fileBytes != null && fileBytes.Length > 0)
                {
                    var base64Data = Convert.ToBase64String(fileBytes);

                    await JS.InvokeVoidAsync("eval", $@"
                        var link = document.createElement('a');
                        link.href = 'data:application/pdf;base64,{base64Data}';
                        link.download = 'EmployeeList.pdf';
                        document.body.appendChild(link);
                        link.click();
                        document.body.removeChild(link);
                        ");
                }
                else
                {
                    await JS.InvokeVoidAsync("alert", "Failed to generate or download the PDF. Check API logs.");
                }
            }
            catch (Exception ex)
            {
                await JS.InvokeVoidAsync("console.error", $"An error occurred: {ex.Message}");
            }
        }
    }
}