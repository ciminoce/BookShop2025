using BookShop2025.Data.Interfaces;
using BookShop2025.Data.Repositories;

namespace BookShop2025.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookShopDbContext _dbContext;
        private ICategoryRepository _categories;
        private ICountryRepository _countries;
        public UnitOfWork(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICategoryRepository Categories 
        {
            get {

                _categories ??= new CategoryRepository(_dbContext);
                return _categories;
            }
        
        
        }
        public ICountryRepository Countries 
        {
            get {

                _countries ??= new CountryRepository(_dbContext);
                return _countries;
            }
        
        
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
    }
}
