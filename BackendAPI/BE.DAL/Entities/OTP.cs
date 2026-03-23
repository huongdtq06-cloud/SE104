namespace BackendAPI.BE.DAL.Entities;

public class OTP
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime Expiration { get; set; }
    public bool IsUsed { get; set; }
}