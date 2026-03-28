namespace BackendAPI.BE.DAL.Entities;

public class VerifyEmailToken
{
    public int VerifyEmailTokenId { get; set; }
    public string Token { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime ExpiresAt { get; set; }

    // Navigation property
    public User? User { get; set; }
}
