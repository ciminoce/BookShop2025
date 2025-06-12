using BookShop2025.Entities.Entities;

namespace BookShop2025.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        void Add(Category category);
        bool Exist(Category category);
        void Edit(Category category);
        void Delete(Category category);

    }

}
