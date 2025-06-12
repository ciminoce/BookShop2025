using BookShop2025.Data;
using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;

namespace BookShop2025.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetAll()
        {
            return _unitOfWork.Categories.GetAll();
        }

        public bool Save(Category category, out List<string> errors)
        {
            errors = new List<string>();
            _unitOfWork.Categories.Add(category);
            int rowsAffected = _unitOfWork.Complete();
            return rowsAffected > 0;
        }
    }
}
