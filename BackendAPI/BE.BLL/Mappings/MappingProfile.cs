using AutoMapper;
using BackendAPI.BE.DAL.Entities;
using BackendAPI.BE.API.DTO;
namespace BackendAPI.BE.BLL.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TestItem, TestItemDTO>();
        CreateMap<TestItemDTO, TestItem>();
    }
}