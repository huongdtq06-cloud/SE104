using BackendAPI.BE.DAL.Interfaces;
using BackendAPI.BE.DAL.Data;
using BackendAPI.BE.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using BackendAPI.BE.BLL.Interfaces;
using BackendAPI.BE.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductSupplierRepository, ProductSupplierRepository>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
