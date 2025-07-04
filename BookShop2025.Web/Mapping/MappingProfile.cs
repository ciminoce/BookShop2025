using AutoMapper;
using BookShop2025.Service.DTOs.Author;
using BookShop2025.Service.DTOs.Category;
using BookShop2025.Service.DTOs.Country;
using BookShop2025.Web.ViewModels.Author;
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
            LoadAuthorMapping();
        }

        private void LoadAuthorMapping()
        {
            CreateMap<AuthorListDto, AuthorListVm>();
            CreateMap<AuthorEditDto, AuthorEditVm>().ReverseMap();
            CreateMap<AuthorEditDto, AuthorListVm>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

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
