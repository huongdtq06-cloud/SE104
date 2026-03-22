namespace BackendAPI.BE.DAL.Entities;


public class PasswordResetToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Expiration { get; set; }
    public int UserId { get; set; }
    public bool IsUsed { get; set; }

    // Navigation property
    public User User { get; set; }
}