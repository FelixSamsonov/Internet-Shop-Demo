using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using InternetShopAspNetCoreMvc.Service.Interfaces;
using InternetShopAspNetCoreMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopAspNetCoreMvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return View(categories);
        }

        public async Task<IActionResult> Manage()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetByIdCategoryAsync(id);

            if (category == null)
            {
                return View("DoesNotExist");
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            var category = new Category
            {
                Name = categoryVM.Name,
                Description = categoryVM.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _categoryService.CreateCategoryAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetByIdCategoryAsync(id);

            if (category == null)
            {
                return View("DoesNotExist");
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            await _categoryService.UpdateCategoryAsync(category);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var category = await _categoryService.GetByIdCategoryAsync(id.Value);

            if (category == null)
            {
                return View("DoesNotExist");
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CategoryProducts(int id)
        {
            var category = await _categoryService.GetByIdCategoryAsync(id);

            if (category == null)
            {
                return View("DoesNotExist");
            }

            return View(category);
        }
    }
}
