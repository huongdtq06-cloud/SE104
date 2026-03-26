namespace BackendAPI.BE.API.Controllers;
using BackendAPI.BE.BLL.Interfaces;
using BackendAPI.BE.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using BackendAPI.BE.API.DTO;
using AutoMapper;
using BackendAPI.BE.DAL.Entities;
using BackendAPI.BE.DAL.Interfaces;

using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService, IHttpContextAccessor httpContextAccessor)
    {
        _productService = productService;
    }
    

    [Authorize]
    [HttpGet("all-products")]
    public IActionResult GetAllProducts()
    {
        
        var products = _productService.GetAllProductsAsync().Result;
        return Ok(products);
    }
}
