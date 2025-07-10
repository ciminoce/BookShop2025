using BookShop2025.Entities.Entities;

namespace BookShop2025.Data.Interfaces
{
    public interface IPublisherRepository
    {
        IQueryable<Publisher> GetAll();
        Publisher? GetById(int id, bool tracked = false);
        void Add(Publisher publisher);
        bool Exist(Publisher publisher);
        void Update(Publisher publisher);
        void Remove(int id);

    }
}
