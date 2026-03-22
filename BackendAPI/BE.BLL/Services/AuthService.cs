namespace BackendAPI.BE.BLL.Services;
using BackendAPI.BE.DAL.Interfaces;
using BackendAPI.BE.API.DTO;
using Microsoft.AspNetCore.Mvc;
using UserModel = BackendAPI.BE.DAL.Entities.User;
using AutoMapper;
using BackendAPI.BE.DAL.Entities;
using BCrypt.Net;

public class AuthService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<bool> LoginAsync(LoginDTO model)
    {
        var user = await _repository.GetByUsernameAsync(model.Username);

        return user != null && BCrypt.Verify(model.Password, user.PasswordHash);
    }

    public async Task<bool> SignupAsync(SignupDTO model)
    {
        if (await _repository.GetByUsernameAsync(model.Username) != null)
            return false; // Username already exists

        var user = new User
        {
            Username = model.Username,
            PasswordHash = BCrypt.HashPassword(model.Password),
            Email = model.Email,
            Phone = model.Phone,
            Address = model.Address
        };

        await _repository.AddAsync(user);
        return true;
    }

}
