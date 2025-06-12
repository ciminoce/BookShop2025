using BookShop2025.Entities.Entities;

namespace BookShop2025.Data.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();

    }

}
