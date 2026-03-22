namespace BackendAPI.BE.DAL.Interfaces;
using BackendAPI.BE.DAL.Entities;
public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
}