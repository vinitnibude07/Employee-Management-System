using EMS.API.Data;
using EMS.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Auth.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, string>
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterCommandHandler(
        AppDbContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var existingUserName = await _context.Users
            .FirstOrDefaultAsync(
                x => x.UserName == request.UserName,
                cancellationToken);

        if (existingUserName != null)
            return "Username already exists.";

        var existingEmail = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email == request.Email,
                cancellationToken);

        if (existingEmail != null)
            return "Email already exists.";

        var user = new User
        {
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PermanentAddress = request.PermanentAddress,
            CurrentAddress = request.CurrentAddress,
            UserName = request.UserName,
            Role = "Admin"
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            request.Password);

        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return "User registered successfully.";
    }
}