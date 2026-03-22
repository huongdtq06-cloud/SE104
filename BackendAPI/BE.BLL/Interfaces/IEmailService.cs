namespace BackendAPI.BE.BLL.Interfaces;
public interface IEmailService {
    Task<string> GenerateOtpAsync();
    Task SendResetPasswordEmailAsync(string toEmail, string resetToken);
}