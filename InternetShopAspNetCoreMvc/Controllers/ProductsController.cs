using AutoMapper;
using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Service.Interfaces;
using InternetShopAspNetCoreMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetShopAspNetCoreMvc.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;


        private const int UserId = 1;

        public ProductsController(
            IProductService productService,
            ICategoryService categoryService,
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _productService.GetAllProductAsync();
            return View(allProducts);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return View("DoesNotExist");
            }
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel productVM)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(
                    await _categoryService.GetAllCategoryAsync(), "Id", "Name",
                    productVM.CategoryId);
                return View(productVM);
            }

            var imagePath = await SaveUploadedFileAsync(productVM);
            var newProduct = _mapper.Map<Product>(productVM);
            newProduct.Image = imagePath;
            newProduct.CreatedAt = DateTime.UtcNow;

            await _productService.CreateProductAsync(newProduct);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Manage()
        {
            var allProducts = await _productService.GetAllProductAsync();
            return View(allProducts);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return View("DoesNotExist");

            var editVM = _mapper.Map<EditProductViewModel>(product);
            ViewData["CategoryId"] = new SelectList(
                await _categoryService.GetAllCategoryAsync(), "Id", "Name",
                product.CategoryId);
            return View(editVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductViewModel productVM)
        {
            if (id != productVM.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(
                    await _categoryService.GetAllCategoryAsync(), "Id", "Name",
                    productVM.CategoryId);
                return View(productVM);
            }

            var existing = await _productService.GetProductByIdAsync(id);
            if (existing == null) return View("DoesNotExist");

            var finalImage = existing.Image;
            if (productVM.Image != null && productVM.Image.Length > 0)
            {
                finalImage = await SaveUploadedFileAsync(productVM);
            }

            _mapper.Map(productVM, existing);
            existing.Image = finalImage;

            await _productService.UpdateProductAsync(existing);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return View("DoesNotExist");
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.Image)
                    && !product.Image.EndsWith("no-image.jpg"))
                {
                    var fullPath = Path.Combine(
                        _webHostEnvironment.WebRootPath,
                        product.Image.Replace('/', Path.DirectorySeparatorChar)
                    );
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                await _productService.DeleteProductAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<string> SaveUploadedFileAsync(IProductViewModelImage vm)
        {
            if (vm.Image == null || vm.Image.Length == 0)
                return "/images/no-image.jpg";

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Products");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(vm.Image.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using var fs = new FileStream(filePath, FileMode.Create);
            await vm.Image.CopyToAsync(fs);

            return $"/images/Products/{uniqueFileName}";
        }



    }
}
