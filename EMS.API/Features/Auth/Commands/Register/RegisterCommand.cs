using EMS.Shared.DTOs;
using MediatR;

namespace EMS.API.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<string>
{
    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string PermanentAddress { get; set; } = string.Empty;

    public string CurrentAddress { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}