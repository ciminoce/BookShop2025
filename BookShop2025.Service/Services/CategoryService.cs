using AutoMapper;
using BookShop2025.Data;
using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using BookShop2025.Service.DTOs.Category;

namespace BookShop2025.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<CategoryListDto> GetAll()
        {
            var categories= _unitOfWork.Categories.GetAll();
            return _mapper.Map<List<CategoryListDto>>(categories);
        }

        public bool Save(CategoryEditDto categoryDto, out List<string> errors)
        {
            errors = new List<string>();
            Category category = _mapper.Map<Category>(categoryDto);
            if (!_unitOfWork.Categories.Exist(category))
            {
                _unitOfWork.Categories.Add(category);
                int rowsAffected = _unitOfWork.Complete();
                return rowsAffected > 0;

            }
            else
            {
                errors.Add("Category Already Exist!!");
                return false;
            } 
        }
    }
}
