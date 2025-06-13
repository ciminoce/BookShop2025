using BookShop2025.Entities.Entities;
using BookShop2025.Service.DTOs.Category;

namespace BookShop2025.Data.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<CategoryListDto> GetAll();
        bool Save(CategoryEditDto categoryDto, out List<string> errors);
    }

}
