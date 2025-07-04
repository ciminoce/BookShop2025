using AutoMapper;
using BookShop2025.Entities.Entities;
using BookShop2025.Service.DTOs.Author;
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
            LoadAuthorMapping();
        }

        private void LoadAuthorMapping()
        {
            CreateMap<Author, AuthorListDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country!.CountryName));
            CreateMap<Author, AuthorEditDto>().ReverseMap();
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
