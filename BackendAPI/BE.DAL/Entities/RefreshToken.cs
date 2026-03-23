namespace BackendAPI.BE.DAL.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = null!;
    public int UserId { get; set; }
    public DateTime ExpiresAt { get; set; }

    // Navigation property
    public User User { get; set; } = null!;
}