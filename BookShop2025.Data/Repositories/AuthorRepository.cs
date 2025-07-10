using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop2025.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookShopDbContext _dbContext;

        public AuthorRepository(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Author author)
        {
            _dbContext.Authors.Add(author);
        }

        public void Remove(int id)
        {
            var authorInDb = GetById(id, tracked:true);
            if (authorInDb is not null)
            {
                _dbContext.Authors.Remove(authorInDb);
            }
        }

        public void Update(Author author)
        {
            var authorInDb = GetById(author.AuthorId,tracked:true);
            if (authorInDb != null)
            {
                authorInDb.FirstName = author.FirstName;
                authorInDb.LastName = author.LastName;
                authorInDb.CountryId = author.CountryId;
                //authorInDb.Country = null;

            }
        }

        public bool Exist(Author author)
        {
            return author.AuthorId == 0
                ? _dbContext.Authors.Any(a => a.FirstName == author.FirstName &&
                        a.LastName == author.LastName &&
                        a.CountryId == author.CountryId)
                : _dbContext.Authors.Any(a => a.FirstName == author.FirstName &&
                        a.LastName == author.LastName &&
                        a.CountryId == author.CountryId &&
                        a.AuthorId != author.AuthorId);
        }

        public IQueryable<Author> GetAll()
        {
            return _dbContext.Authors.Include(a=>a.Country)
                .AsNoTracking();

        }

        public Author? GetById(int id, bool tracked = false)
        {
            if (tracked)
            {
                return _dbContext.Authors.Include(a => a.Country)
                    .FirstOrDefault(a => a.AuthorId == id);
            }
            return _dbContext.Authors.Include(a => a.Country)
                .AsNoTracking()
                .FirstOrDefault(a => a.AuthorId == id);
        }
    }
}
