using AutoMapper;
using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using BookShop2025.Web.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace BookShop2025.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var categoriesDto = _categoryService.GetAll();
            var categoriesVm=_mapper.Map<List<CategoryListVm>>(categoriesDto);
            return View(categoriesVm);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CatetoryEditVm categoryVm)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category { CategoryName = categoryVm.CategoryName };
                try
                {
                    if (_categoryService.Save(category, out var errors))
                    {
                        TempData["success"] = "Register Successfully Added";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, errors.First());
                    }
                    return View(categoryVm);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return View(categoryVm);
        }
    }
}
