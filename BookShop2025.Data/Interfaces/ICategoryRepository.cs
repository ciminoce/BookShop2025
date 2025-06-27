using BookShop2025.Entities.Entities;

namespace BookShop2025.Data.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAll();
        Category? GetById(int id);
        void Add(Category category);
        bool Exist(Category category);
        void Update(Category category);
        void Remove(int id);

    }

}
