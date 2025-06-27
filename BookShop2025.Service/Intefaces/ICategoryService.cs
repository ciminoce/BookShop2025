using BookShop2025.Service.DTOs.Category;

namespace BookShop2025.Data.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<CategoryListDto> GetAll();
        bool Save(CategoryEditDto categoryDto, out List<string> errors);
        CategoryEditDto? GetById(int id);
        bool Remove(int id, out List<string> errors);
    }

}
