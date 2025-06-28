using AutoMapper;
using BookShop2025.Entities.Entities;
using BookShop2025.Service.DTOs.Category;
using BookShop2025.Service.DTOs.Country;

namespace BookShop2025.Service.Mapping
{
    public class MappingProfileDto : Profile
    {
        public MappingProfileDto()
        {
            LoadCategoryMapping();
            LoadCountryMapping();
        }

        private void LoadCountryMapping()
        {
            CreateMap<Country, CountryListDto>();
            CreateMap<Country, CountryEditDto>().ReverseMap();
        }

        private void LoadCategoryMapping()
        {
            CreateMap<Category, CategoryListDto>();
            CreateMap<Category, CategoryEditDto>().ReverseMap();
        }
    }
}
