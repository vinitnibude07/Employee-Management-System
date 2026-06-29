using EMS.API.Data;
using EMS.API.Services.Auth;
using EMS.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Auth.Commands.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, string>
{
    private readonly AppDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public LoginCommandHandler(
        AppDbContext context,
        IJwtService jwtService,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(
    LoginCommand request,
    CancellationToken cancellationToken)
    {
        Console.WriteLine($"Username entered: {request.UserName}");

        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.UserName == request.UserName,
                cancellationToken);

        if (user == null)
        {
            Console.WriteLine("User not found.");
            return string.Empty;
        }

        Console.WriteLine($"User found: {user.UserName}");
        Console.WriteLine($"Stored hash: {user.PasswordHash}");

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        Console.WriteLine($"Password result: {result}");

        if (result == PasswordVerificationResult.Failed)
        {
            Console.WriteLine("Password verification failed.");
            return string.Empty;
        }

        Console.WriteLine("Login successful.");

        return _jwtService.GenerateToken(user);
    }
}