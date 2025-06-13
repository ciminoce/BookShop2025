using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
using BookShop2025.Web.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace BookShop2025.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
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
