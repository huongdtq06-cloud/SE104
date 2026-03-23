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
    private readonly IOTPRepository _OTPRepository;
    private readonly IPasswordResetTokenRepository _PasswordResetTokenRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;
    public AuthService(IUserRepository userRepository, IOTPRepository OTPRepository, IPasswordResetTokenRepository PasswordResetTokenRepository,
     IMapper mapper, IEmailService emailService, ITokenService tokenService, IRepository<RefreshToken> refreshTokenRepository)
    {
        _userRepository = userRepository;
        _OTPRepository = OTPRepository;
        _PasswordResetTokenRepository = PasswordResetTokenRepository;
        _mapper = mapper;
        _emailService = emailService;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
    }
    public async Task<TokenDTO> LoginAsync(LoginDTO model)
    {
        var user = await _userRepository.GetByUsernameAsync(model.Username);
        if (user==null) return null;
        var userDTO = _mapper.Map<UserDTO>(user);
        string accessToken = _tokenService.CreateAccessToken(userDTO);
        string refreshToken = _tokenService.GenerateRandomStringToken();
        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(7) // Set expiration as needed
        };
        await _refreshTokenRepository.AddAsync(refreshTokenEntity);

        return new TokenDTO { AccessToken = accessToken, RefreshToken = refreshToken };
    }

    public async Task<bool> SignupAsync(SignupDTO model)
    {
        if (await _userRepository.GetByUsernameAsync(model.Username) != null)
            return false; // Username already exists

        

        var user = _mapper.Map<User>(model);
        user.PasswordHash = BCrypt.HashPassword(model.Password);

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
        var OTP = new OTP
        {
            Code = otpCode,
            Email = model.Email,
            CreatedAt = DateTime.UtcNow,
            Expiration = DateTime.UtcNow.AddMinutes(15), // OTP valid for 15 minutes
            IsUsed = false
        };
        await _OTPRepository.AddAsync(OTP);


        return true;
    }

    public async Task<string> verifyOtpAsync(string otp, string email) {
        var otpEntity = await _OTPRepository.GetByEmailAsync(email);
        if(otpEntity == null || otpEntity.Expiration < DateTime.UtcNow || otpEntity.IsUsed) return null;
        if(otpEntity.Code != otp) return null;

        var resetPasswordToken = _tokenService.GenerateRandomStringToken();
        var tokenEntity = new PasswordResetToken
        {
            Token = resetPasswordToken,
            Email = email,
            CreatedAt = DateTime.UtcNow,
            Expiration = DateTime.UtcNow.AddHours(1),
             
        };
        await _PasswordResetTokenRepository.AddAsync(tokenEntity);
        await _OTPRepository.MarkAsUsedAsync(otpEntity.Id);
        return resetPasswordToken;
    }

    public async Task<bool> ResetPasswordAsync(ChangePasswordDTO model)
    {
        var tokenEntity = await _PasswordResetTokenRepository.GetByTokenAsync(model.resetPassToken);
        if(tokenEntity == null || tokenEntity.Expiration < DateTime.UtcNow) return false;

        var user = await _userRepository.GetByEmailAsync(tokenEntity.Email);
        if(user == null) return false;

        user.PasswordHash = BCrypt.HashPassword(model.newPass);
        await _userRepository.UpdateAsync(user);
        return true;
    }

}
