using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using Microsoft.EntityFrameworkCore;

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

        public void Remove(int id)
        {
            //TODO: Ver luego cuando esté relacionada
            var categoryInDb = GetById(id);
            if (categoryInDb is not null)
            {
                _dbContext.Entry(categoryInDb).State = EntityState.Deleted;
            }
        }

        public void Update(Category category)
        {
            var categoryInDb=GetById(category.CategoryId);
            if (categoryInDb != null)
            {
                categoryInDb.CategoryName = category.CategoryName;
                categoryInDb.Description = category.Description;
                categoryInDb.IsActive = category.IsActive;

                _dbContext.Entry(categoryInDb).State=EntityState.Modified;
            }
        }

        public bool Exist(Category category)
        {
            return category.CategoryId == 0
                ? _dbContext.Categories.Any(c => c.CategoryName == category.CategoryName)
                : _dbContext.Categories.Any(c => c.CategoryName == category.CategoryName &&
                    c.CategoryId != category.CategoryId);
        }

        public IQueryable<Category> GetAll()
        {
            return _dbContext.Categories
                .AsNoTracking();
                
        }

        public Category? GetById(int id)
        {
            return _dbContext.Categories
                .AsNoTracking()
                .FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
