using AutoMapper;
using Test.Api.Application.Features.Commands.Category.Create;
using Test.Api.Domain.Models;

namespace Test.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<Category, CreateCategoryCommand>()
            .ReverseMap();

         
        }

        
    }
}
