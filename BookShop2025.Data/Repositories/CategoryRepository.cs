using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;

namespace BookShop2025.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookShopDbContext _dbContext;

        public CategoryRepository(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Category category)
        {
            _dbContext.Categories.Add(category);
        }

        public void Delete(Category category)
        {
            throw new NotImplementedException();
        }

        public void Edit(Category category)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Category category)
        {
            return category.CategoryId == 0
                ? _dbContext.Categories.Any(c => c.CategoryName == category.CategoryName)
                : _dbContext.Categories.Any(c => c.CategoryName == category.CategoryName &&
                    c.CategoryId != category.CategoryId);
        }

        public IEnumerable<Category> GetAll()
        {
            return _dbContext.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
