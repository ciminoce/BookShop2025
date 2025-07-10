using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop2025.Data.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookShopDbContext _dbContext;

        public PublisherRepository(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Publisher publisher)
        {
            _dbContext.Publishers.Add(publisher);
        }

        public void Remove(int id)
        {
            var publisherInDb = GetById(id, tracked:true);
            if (publisherInDb is not null)
            {
                _dbContext.Publishers.Remove(publisherInDb);
            }
        }

        public void Update(Publisher publisher)
        {
            var publisherInDb = GetById(publisher.PublisherId,tracked:true);
            if (publisherInDb != null)
            {
                publisherInDb.Name = publisher.Name;
                publisherInDb.CountryId = publisher.CountryId;
                //publisherInDb.Country = null;

            }
        }

        public bool Exist(Publisher publisher)
        {
            return publisher.PublisherId == 0
                ? _dbContext.Publishers.Any(a => a.Name == publisher.Name &&
                        a.CountryId == publisher.CountryId)
                : _dbContext.Publishers.Any(a => a.Name == publisher.Name &&
                        a.CountryId == publisher.CountryId &&
                        a.PublisherId != publisher.PublisherId);
        }

        public IQueryable<Publisher> GetAll()
        {
            return _dbContext.Publishers.Include(a=>a.Country)
                .AsNoTracking();

        }

        public Publisher? GetById(int id, bool tracked = false)
        {
            if (tracked)
            {
                return _dbContext.Publishers.Include(a => a.Country)
                    .FirstOrDefault(a => a.PublisherId == id);
            }
            return _dbContext.Publishers.Include(a => a.Country)
                .AsNoTracking()
                .FirstOrDefault(a => a.PublisherId == id);
        }
    }
}
