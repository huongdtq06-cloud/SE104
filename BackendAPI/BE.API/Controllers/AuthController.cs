namespace BackendAPI.Controllers;

using BackendAPI.BE.DAL.Interfaces;
using BackendAPI.BE.API.DTO;
using BackendAPI.BE.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using UserModel = BackendAPI.BE.DAL.Entities.User;
using AutoMapper;
using BackendAPI.BE.DAL.Entities;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly AuthService _authService;

    public AuthController(IMapper mapper, IUserRepository repository)
    {        
        _authService = new AuthService(repository, mapper);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO model)
    {
        var result = await _authService.LoginAsync(model);
        if (!result)
            return Unauthorized(new { Success = false, Message = "Invalid username or password." });
        return Ok(new { Success = result });
    }   
    
}