using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace BigStoreApp.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTOInsertion>().ReverseMap();
            CreateMap<Product, ProductDTOUpdate>().ReverseMap();
            CreateMap<IdentityUser, UserDTOForInsertion>().ReverseMap();

        }
    }
}
