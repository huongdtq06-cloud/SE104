using BackendAPI.BE.DAL.Data;
using BackendAPI.BE.DAL.Entities;
using BackendAPI.BE.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BackendAPI.BE.DAL.Repositories;

public class PasswordResetTokenRepository : Repository<PasswordResetToken>, IPasswordResetTokenRepository
{
    public PasswordResetTokenRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<PasswordResetToken> GetByTokenAsync(string token)
    {
        return await _context.PasswordResetTokens.FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task DeleteExpiredTokensAsync()
    {
        var expiredTokens = await _context.PasswordResetTokens.Where(t => t.Expiration < DateTime.UtcNow).ToListAsync();
        _context.PasswordResetTokens.RemoveRange(expiredTokens);
        await _context.SaveChangesAsync();
    }
}