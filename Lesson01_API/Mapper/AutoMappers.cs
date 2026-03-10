using AutoMapper;
using Lesson01_API.DTOs.Request;
using Lesson01_API.DTOs.Response;
using Lesson01_API.Models;

namespace Lesson01_API.Mapper
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            // Mapping for Category
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.CategoryName : null));
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>();
        }
    }
}
