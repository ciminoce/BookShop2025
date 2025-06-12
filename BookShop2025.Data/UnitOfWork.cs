using BookShop2025.Data.Interfaces;

namespace BookShop2025.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookShopDbContext _dbContext;

        public UnitOfWork(BookShopDbContext dbContext, ICategoryRepository categories)
        {
            _dbContext = dbContext;
            Categories = categories;
        }

        public ICategoryRepository Categories { get; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
    }
}
