namespace BackendAPI.BE.DAL.Interfaces;
using BackendAPI.BE.DAL.Entities;
public interface IPasswordResetTokenRepository : IRepository<PasswordResetToken>
{
    Task<PasswordResetToken> GetByTokenAsync(string token);
    Task DeleteExpiredTokensAsync();
}