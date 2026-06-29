using System;

namespace EMS.Blazor.Services
{
    public class TokenService
    {
        private string? _token;
        private string? _username; // Added to store the logged-in user's name
        public Guid InstanceId { get; } = Guid.NewGuid();

        public void SetToken(string token)
        {
            _token = token;
        }

        public string? GetToken()
        {
            return _token;
        }

        public void SetUsername(string username)
        {
            _username = username;
        }

        public string? GetUsername()
        {
            return _username;
        }

        public void ClearToken()
        {
            _token = null;
            _username = null;
        }
    }
}