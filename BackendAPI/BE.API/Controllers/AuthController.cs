namespace BackendAPI.Controllers;

using BackendAPI.BE.DAL.Interfaces;
using BackendAPI.BE.API.DTO;
using BackendAPI.BE.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BackendAPI.BE.DAL.Entities;
using BackendAPI.BE.BLL.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;


    public AuthController(IMapper mapper, IUserRepository repository, IEmailService emailService, IRepository<PasswordResetToken> tokenService)
    {        
        _authService = new AuthService(repository,tokenService, mapper, emailService);        
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO model)
    {
        var result = await _authService.LoginAsync(model);
        if (!result)
            return Unauthorized(new { Success = false, Message = "Invalid username or password." });
        return Ok(new { Success = result });
    }   

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SignupDTO model)
    {
        var result = await _authService.SignupAsync(model);
        if (!result)
            return BadRequest(new { Success = false, Message = "Username already exists." });
        return Ok(new { Success = result });
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
    {
        var result = await _authService.ForgotPasswordAsync(model);
        if (!result)
            return BadRequest(new { Success = false, Message = "Email not found." });
        return Ok(new { Success = true });
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpDTO model)
    {
        var result = await _authService.verifyOtpAsync(model.Otp, model.Otp);
        if (!result)
            return BadRequest(new { Success = false, Message = "Invalid OTP." });
        return Ok(new { Success = true });
    }
}