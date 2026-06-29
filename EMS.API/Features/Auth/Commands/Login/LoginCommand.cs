using MediatR;

namespace EMS.API.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<string>
{
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}