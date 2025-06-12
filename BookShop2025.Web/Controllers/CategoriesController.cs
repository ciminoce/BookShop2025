using BookShop2025.Data.Interfaces;
using BookShop2025.Entities.Entities;
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
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_categoryService.Save(category, out var errors))
                    {
                        TempData["success"] = "Register Successfully Added";
                        return RedirectToAction("Index");
                    }
                    return View(category);
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return View(category);
        }
    }
}
