using System.Net.Http.Json;
using EMS.Shared.DTOs;

namespace EMS.Blazor.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> RegisterAsync(RegisterRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "https://localhost:7106/api/Auth/register",
            request);

        return response.IsSuccessStatusCode;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "https://localhost:7106/api/Auth/login",
            request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content
            .ReadFromJsonAsync<LoginResponseDto>();
    }
}