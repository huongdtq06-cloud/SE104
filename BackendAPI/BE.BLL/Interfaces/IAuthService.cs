namespace BackendAPI.BE.BLL.Interfaces;
using UserModel = BackendAPI.BE.API.DTO.UserDTO;
using BackendAPI.BE.API.DTO;
public interface IAuthService
{
    Task<bool> LoginAsync(LoginDTO model);
    Task<bool> SignupAsync(SignupDTO model);
    Task<bool> ForgotPasswordAsync(ForgotPasswordDTO model);
    
    Task<bool> verifyOtpAsync(string email_otp, string otp);
}