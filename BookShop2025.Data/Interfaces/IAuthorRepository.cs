using BookShop2025.Entities.Entities;

namespace BookShop2025.Data.Interfaces
{
    public interface IAuthorRepository
    {
        IQueryable<Author> GetAll();
        Author? GetById(int id);
        void Add(Author author);
        bool Exist(Author author);
        void Update(Author author);
        void Remove(int id);

    }

}
