namespace BackendAPI.BE.BLL.Services;
using BackendAPI.BE.DAL.Interfaces;
using BackendAPI.BE.API.DTO;
using AutoMapper;
using BackendAPI.BE.DAL.Entities;
using BCrypt.Net;
using BackendAPI.BE.BLL.Interfaces;
using BackendAPI.BE.DAL.Repositories;

public class AuthService: IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRepository<PasswordResetToken> _tokenService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public AuthService(IUserRepository userRepository, IRepository<PasswordResetToken> tokenService, IMapper mapper, IEmailService emailService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mapper = mapper;
        _emailService = emailService;
    }
    public async Task<bool> LoginAsync(LoginDTO model)
    {
        var user = await _userRepository.GetByUsernameAsync(model.Username);

        return user != null && BCrypt.Verify(model.Password, user.PasswordHash);
    }

    public async Task<bool> SignupAsync(SignupDTO model)
    {
        if (await _userRepository.GetByUsernameAsync(model.Username) != null)
            return false; // Username already exists

        var user = new User
        {
            Username = model.Username,
            PasswordHash = BCrypt.HashPassword(model.Password),
            Email = model.Email,
            Phone = model.Phone,
            Address = model.Address
        };

        await _userRepository.AddAsync(user);
        return true;
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordDTO model)
    {
        var user = await _userRepository.GetByEmailAsync(model.Email);
        if (user == null)
            return false; // Email not found
        var otpCode = await _emailService.GenerateOtpAsync();

        await _emailService.SendResetPasswordEmailAsync(model.Email, otpCode);
        var passwordResetToken = new PasswordResetToken
        {
            Email = model.Email,
            Token = otpCode,
            CreatedAt = DateTime.UtcNow,
            Expiration = DateTime.UtcNow.AddMinutes(5),
            UserId = user.UserId,
            IsUsed = false
        };
        await _tokenService.AddAsync(passwordResetToken);


        return true;
    }

    public async Task<bool> verifyOtpAsync(string email_otp, string otp) {
        return email_otp == otp;
    }

}
