namespace BackendAPI.BE.BLL.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System.Security.Cryptography;


public class EmailService : IEmailService {
    public async Task<string> GenerateOtpAsync() {
        int otp= RandomNumberGenerator.GetInt32(100000, 999999);
        return otp.ToString();
    }

    

    public async Task SendResetPasswordEmailAsync(string toEmail, string otpCode) {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Stockify", "stockify.support@gmail.com"));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = "Khôi phục mật khẩu tài khoản Staff";

        
        message.Body = new TextPart("html") {
        Text = $@"
            <div style='font-family: sans-serif; text-align: center;'>
                <h3>Mã xác thực của bạn là:</h3>
                <h1 style='color: #007bff; letter-spacing: 5px;'>{otpCode}</h1>
                <p>Mã này có hiệu lực trong <b>5 phút</b>.</p>
                <p>Vui lòng không cung cấp mã này cho bất kỳ ai.</p>
            </div>"
    };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        
        
        await client.AuthenticateAsync("stockify.support@gmail.com", "pkzh tkgk qapt xrpr");
        
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
