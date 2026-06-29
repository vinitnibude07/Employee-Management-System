using Microsoft.AspNetCore.Components;
using EMS.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Blazor.Components.Pages
{
    public partial class Home
    {
        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Inject]
        private TokenService TokenService { get; set; } = default!;

        [Inject]
        private EmployeeService EmployeeService { get; set; } = default!;

        public bool IsAdmin { get; set; } = false;

        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(TokenService.GetToken());

        public string CurrentUser => !string.IsNullOrWhiteSpace(TokenService.GetUsername())
            ? TokenService.GetUsername()!
            : (IsLoggedIn ? "Authorized User" : "Guest User");

        // UI Metric Properties
        public int TotalEmployeesCount { get; set; } = 0;
        public int ActiveEmployeesCount { get; set; } = 0;
        public int DepartmentCount { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            // FIXED: Runs automatically on load, showing numbers before logging in
            await FetchLiveMetricsAsync();
        }

        /// <summary>
        /// FIXED: Aggregates across paged responses of size 5 to find absolute totals 
        /// safely without using non-existent DTO properties.
        /// </summary>
        private async Task FetchLiveMetricsAsync()
        {
            try
            {
                int currentPage = 1;
                bool hasMoreRecords = true;

                // FIXED: Changed from List<bool?> to List<bool> to match your exact DTO model type
                var allActiveStates = new List<bool>();
                var allDepartments = new List<string>();
                int totalAccumulatedCount = 0;

                while (hasMoreRecords)
                {
                    // Pull the database page sequentially
                    var response = await EmployeeService.GetEmployeesAsync(currentPage, "", "");

                    if (response != null && response.Items != null && response.Items.Any())
                    {
                        totalAccumulatedCount += response.Items.Count;

                        // FIXED: This now compiles perfectly with no conversion errors!
                        allActiveStates.AddRange(response.Items.Select(e => e.IsActive));

                        // Track department designations across the page
                        allDepartments.AddRange(response.Items.Select(e => e.DepartmentName));

                        // If the page contains fewer than 5 items, we have hit the end of the data collection
                        if (response.Items.Count < 5)
                        {
                            hasMoreRecords = false;
                        }
                        else
                        {
                            currentPage++;
                        }
                    }
                    else
                    {
                        hasMoreRecords = false;
                    }
                }

                // Map aggregated true totals to the front-facing dashboard elements
                TotalEmployeesCount = totalAccumulatedCount;

                // Simplified status check since elements are now straight booleans
                ActiveEmployeesCount = allActiveStates.Count(status => status);

                DepartmentCount = allDepartments.Where(name => !string.IsNullOrWhiteSpace(name))
                                                .Distinct()
                                                .Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Dashboard synchronization anomaly caught: {ex.Message}");
            }
        }

        private void SecureNavigateTo(string targetPath)
        {
            if (!IsLoggedIn)
            {
                Navigation.NavigateTo("/login");
            }
            else
            {
                Navigation.NavigateTo(targetPath);
            }
        }

        private void NavigateToLogin() => Navigation.NavigateTo("/login");
        private void NavigateToRegister() => Navigation.NavigateTo("/register");

        private void LogOut()
        {
            TokenService.ClearToken();
            Navigation.NavigateTo("/", forceLoad: true);
        }
    }
}