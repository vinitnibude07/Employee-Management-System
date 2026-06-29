using EMS.Shared.Models;

namespace EMS.API.Services.Auth
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}