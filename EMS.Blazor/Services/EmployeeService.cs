using System.Net.Http.Headers;
using System.Net.Http.Json;
using EMS.Shared.DTOs;

namespace EMS.Blazor.Services;

public class EmployeeService
{
    private readonly HttpClient _httpClient;
    private readonly TokenService _tokenService;

    public EmployeeService(HttpClient httpClient, TokenService tokenService)
    {
        _httpClient = httpClient;
        _tokenService = tokenService;
    }

    private void SetAuthorizationHeader()
    {
        var token = _tokenService.GetToken();

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public async Task<PagedResultDto<EmployeeResponseDto>?> GetEmployeesAsync(int pageNumber, string sortBy, string searchText)
    {
        try
        {
            var token = _tokenService.GetToken();
            if (!string.IsNullOrWhiteSpace(token))
            {
                SetAuthorizationHeader();
            }

            return await _httpClient.GetFromJsonAsync<PagedResultDto<EmployeeResponseDto>>(
                $"https://localhost:7106/api/Employee/paged?pageNumber={pageNumber}&pageSize=5&sortBy={sortBy}&searchText={searchText}");
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> AddEmployeeAsync(EmployeeCreateDto employee)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7106/api/Employee", employee);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.DeleteAsync($"https://localhost:7106/api/Employee/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateEmployeeAsync(EmployeeUpdateDto employee)
    {
        SetAuthorizationHeader();
        var response = await _httpClient.PutAsJsonAsync("https://localhost:7106/api/Employee", employee);
        return response.IsSuccessStatusCode;
    }

    public async Task<EmployeeResponseDto?> GetEmployeeByIdAsync(int id)
    {
        SetAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<EmployeeResponseDto>($"https://localhost:7106/api/Employee/{id}");
    }

    public async Task<byte[]> DownloadEmployeePdfAsync()
    {
        SetAuthorizationHeader();
        var response = await _httpClient.GetAsync("https://localhost:7106/api/Employee/download-pdf");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }
        return null;
    }

    public async Task<List<DepartmentResponseDto>?> GetDepartmentsAsync()
    {
        SetAuthorizationHeader();
        return await _httpClient.GetFromJsonAsync<List<DepartmentResponseDto>>("https://localhost:7106/api/Departments");
    }
}