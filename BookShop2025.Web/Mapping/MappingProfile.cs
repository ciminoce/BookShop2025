using AutoMapper;
using BookShop2025.Service.DTOs.Category;
using BookShop2025.Service.DTOs.Country;
using BookShop2025.Web.ViewModels.Category;
using BookShop2025.Web.ViewModels.Country;

namespace BookShop2025.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadCategoryMapping();
            LoadCountryMapping();
        }

        private void LoadCountryMapping()
        {
            CreateMap<CountryListDto, CountryListVm>();
            CreateMap<CountryEditVm, CountryEditDto>().ReverseMap();
        }

        private void LoadCategoryMapping()
        {
            CreateMap<CategoryListDto, CategoryListVm>();
            CreateMap<CategoryEditVm, CategoryEditDto>().ReverseMap();
        }
    }
}
