using AutoMapper;
using BookShop2025.Service.DTOs.Category;
using BookShop2025.Web.ViewModels.Category;

namespace BookShop2025.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadCategoryMapping();
        }

        private void LoadCategoryMapping()
        {
            CreateMap<CategoryListDto, CategoryListVm>();
            CreateMap<CategoryEditVm, CategoryEditDto>();
        }
    }
}
