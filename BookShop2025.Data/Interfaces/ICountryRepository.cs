using BookShop2025.Entities.Entities;

namespace BookShop2025.Data.Interfaces
{
    public interface ICountryRepository
    {
        IQueryable<Country> GetAll();
        Country? GetById(int id);
        void Add(Country category);
        bool Exist(Country category);
        void Update(Country category);
        void Remove(int id);

    }

}
