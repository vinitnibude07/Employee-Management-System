using EMS.API.Data;
using EMS.API.Features.Auth.Commands.ForgotPassword;
using EMS.API.Features.Auth.Commands.Login;
using EMS.API.Features.Auth.Commands.Register;
using EMS.API.Features.Auth.Commands.ResetPassword;
using EMS.API.Services;
using EMS.API.Services.Auth;
using EMS.Shared.DTOs;
using EMS.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        private readonly IJwtService _jwtService;
        private readonly PasswordHasher<User> _passwordHasher;

        private readonly IMediator _mediator;

        public AuthController(
            IMediator mediator,
            AppDbContext context,
            IJwtService jwtService,
            EmailService emailService)
        {
            _mediator = mediator;
            _context = context;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(
                new RegisterCommand
                {
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    PermanentAddress = request.PermanentAddress,
                    CurrentAddress = request.CurrentAddress,
                    UserName = request.UserName,
                    Password = request.Password
                });

            if (result == "Username already exists." ||
                result == "Email already exists.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _mediator.Send(
            new LoginCommand
            {
                UserName = request.UserName,
                Password = request.Password
            });

            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized("Invalid email or password.");

            return Ok(new
            {
                Token = token
            });
        }

        [HttpGet("test-email")]
        public async Task<IActionResult> TestEmail()
        {
            await _emailService.SendEmailAsync(
                "vinit.alohatech@gmail.com",
                "EMS Email Test",
                "Congratulations! Your EMS email service is working.");

            return Ok("Email sent successfully.");
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(
        ForgotPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

                var result = await _mediator.Send(
                new ForgotPasswordCommand
                {
                    Email = request.Email
                });

            if (result == "User not found.")
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(
    ResetPasswordRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(
                new ResetPasswordCommand
                {
                    Email = request.Email,
                    Otp = request.Otp,
                    NewPassword = request.NewPassword
                });

            if (result == "User not found." ||
                result == "OTP not found." ||
                result == "Invalid OTP." ||
                result == "OTP has expired.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

