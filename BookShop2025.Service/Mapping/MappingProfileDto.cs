using AutoMapper;
using BookShop2025.Entities.Entities;
using BookShop2025.Service.DTOs.Category;

namespace BookShop2025.Service.Mapping
{
    public class MappingProfileDto : Profile
    {
        public MappingProfileDto()
        {
            LoadCategoryMapping();
        }

        private void LoadCategoryMapping()
        {
            CreateMap<Category, CategoryListDto>();
            CreateMap<CategoryEditDto, Category>();
        }
    }
}
