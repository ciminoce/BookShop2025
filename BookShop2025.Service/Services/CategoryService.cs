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

        public IQueryable<CategoryListDto> GetAll()
        {
            var categories= _unitOfWork.Categories.GetAll();
            return _mapper.ProjectTo<CategoryListDto>(categories);
        }

        public CategoryEditDto? GetById(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);
            if (category is null) return null;
            return _mapper.Map<CategoryEditDto>(category);
        }

        public bool Remove(int id, out List<string> errors)
        {
            errors = new List<string>();
            var category = _unitOfWork.Categories.GetById(id);
            if (category is null)
            {
                errors.Add("Category does not exist");
                return false;
            }
            _unitOfWork.Categories.Remove(id);
            var rowsAffected = _unitOfWork.Complete();
            return rowsAffected > 0;

        }

        public bool Save(CategoryEditDto categoryDto, out List<string> errors)
        {
            errors = new List<string>();
            Category category = _mapper.Map<Category>(categoryDto);
            if (category.CategoryId==0)
            {
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
            else
            {
                if (!_unitOfWork.Categories.Exist(category))
                {
                    _unitOfWork.Categories.Update(category);
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
}
