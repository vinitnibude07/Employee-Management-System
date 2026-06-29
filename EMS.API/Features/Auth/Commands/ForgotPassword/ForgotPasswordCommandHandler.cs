using EMS.API.Data;
using EMS.API.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand, string>
{
    private readonly AppDbContext _context;
    private readonly EmailService _emailService;

    public ForgotPasswordCommandHandler(
        AppDbContext context,
        EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public async Task<string> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email == request.Email,
                cancellationToken);

        if (user == null)
            return "User not found.";

        var otp = new Random()
            .Next(100000, 999999)
            .ToString();

        user.PasswordResetOtp = otp;
        user.PasswordResetOtpExpiry = DateTime.UtcNow.AddMinutes(10);

        await _context.SaveChangesAsync(cancellationToken);

        Console.WriteLine($"Sending OTP to: '{user.Email}'");

        await _emailService.SendEmailAsync(
        user.Email,
        "EMS Password Reset.",
        $"Your One Time Password (OTP) is : {otp}");

        return "OTP sent successfully.";
    }
}