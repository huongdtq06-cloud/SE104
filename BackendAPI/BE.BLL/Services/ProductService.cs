namespace BackendAPI.BE.BLL.Services;

using AutoMapper;
using BackendAPI.BE.DAL.Entities;
using BackendAPI.BE.DAL.Repositories;
using BackendAPI.BE.BLL.Interfaces;
using BackendAPI.BE.API.DTO;

// public class ProductService : IProductService
// {
//     private readonly IProductRepository _productRepository;
//     private readonly IMapper _mapper;

//     public ProductService(IProductRepository productRepository, IMapper mapper)
//     {
//         _productRepository = productRepository;
//         _mapper = mapper;
//     }

//     public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
//     {
//         var products = await _productRepository.GetAllAsync();
//         return _mapper.Map<IEnumerable<ProductDTO>>(products);
//     }
// }
