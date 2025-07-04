using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop2025.Data.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly BookShopDbContext _dbContext;

        public CountryRepository(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Country country)
        {
            _dbContext.Countries.Add(country);
        }

        public void Remove(int id)
        {
            var countryInDb = GetById(id);
            if (countryInDb != null)
            {
                _dbContext.Entry(countryInDb).State = EntityState.Deleted;
            }
        }

        public void Update(Country country)
        {
            var countryInDb=GetById(country.CountryId);
            if (countryInDb != null)
            {
                countryInDb.CountryName = country.CountryName;

                _dbContext.Entry(countryInDb).State = EntityState.Modified;
            }
        }

        public bool Exist(Country country)
        {
            return country.CountryId == 0
                ? _dbContext.Countries.Any(c => c.CountryName == country.CountryName)
                : _dbContext.Countries.Any(c => c.CountryName == country.CountryName &&
                    c.CountryId != country.CountryId);
        }

        public IQueryable<Country> GetAll()
        {

            IQueryable<Country> query=_dbContext.Countries.AsNoTracking()
                .OrderBy(c=>c.CountryName);
            return query;
        }

        public Country? GetById(int id)
        {
            return _dbContext.Countries.AsNoTracking()
                .FirstOrDefault(c=>c.CountryId==id);
        }
    }
}
