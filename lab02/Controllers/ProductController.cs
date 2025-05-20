using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using WebsiteBanHang.Models;
using WebsiteBanHang.Repositories;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _catRepo;
        private readonly IWebHostEnvironment _env;

        public ProductController(
            IProductRepository repo,
            ICategoryRepository catRepo,
            IWebHostEnvironment env)
        {
            _repo = repo;
            _catRepo = catRepo;
            _env = env;
        }

        // Đổ dropdown danh mục
        private void PopulateCategories(int? selectedId = null)
        {
            ViewBag.Categories = _catRepo.GetAllCategories()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name,
                    Selected = (selectedId != null && c.Id == selectedId)
                })
                .ToList();
        }

        // GET: /Product
        public IActionResult Index()
        {
            var products = _repo.GetAll().ToList();
            var categories = _catRepo.GetAllCategories().ToList();
            foreach (var p in products)
                p.Category = categories.FirstOrDefault(c => c.Id == p.CategoryId);
            return View(products);
        }

        // GET: /Product/Details/5
        public IActionResult Details(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            p.Category = _catRepo.GetAllCategories()
                                 .FirstOrDefault(c => c.Id == p.CategoryId);
            return View(p);
        }
        // GET: /Product/Create
        [HttpGet]
        public IActionResult Create()
        {
            PopulateCategories();
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        public IActionResult Create(Product model, IFormFile? image)
        {
            PopulateCategories(model.CategoryId);
            if (!ModelState.IsValid) return View(model);
            // xử lý ảnh + lưu
            _repo.Add(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Product/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            PopulateCategories(p.CategoryId);
            return View(p);
        }

        // POST: /Product/Edit/5
        [HttpPost]
        public IActionResult Edit(Product model, IFormFile? image)
        {
            PopulateCategories(model.CategoryId);

            if (!ModelState.IsValid)
                return View(model);

            if (image != null && image.Length > 0)
            {
                var path = Path.Combine(_env.WebRootPath, "images", image.FileName);
                using var fs = new FileStream(path, FileMode.Create);
                image.CopyTo(fs);
                model.ImageUrl = "/images/" + image.FileName;
            }

            _repo.Update(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Product/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var p = _repo.GetById(id);
            if (p == null) return NotFound();
            p.Category = _catRepo.GetAllCategories()
                                 .FirstOrDefault(c => c.Id == p.CategoryId);
            return View(p);
        }

        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
