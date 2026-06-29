using EMS.API.Data;
using EMS.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Auth.Commands.ResetPassword;

public class ResetPasswordCommandHandler
    : IRequestHandler<ResetPasswordCommand, string>
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public ResetPasswordCommandHandler(
        AppDbContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email == request.Email,
                cancellationToken);

        if (user == null)
            return "User not found.";

        if (string.IsNullOrWhiteSpace(user.PasswordResetOtp))
            return "OTP not found.";

        if (user.PasswordResetOtp != request.Otp)
            return "Invalid OTP.";

        if (user.PasswordResetOtpExpiry < DateTime.UtcNow)
            return "OTP has expired.";

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            request.NewPassword);

        user.PasswordResetOtp = null;
        user.PasswordResetOtpExpiry = null;

        await _context.SaveChangesAsync(cancellationToken);

        return "Password reset successfully.";
    }
}