using BulkyWeb.Controllers.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext db)
        {
            _dbContext = db;
        }
        public IActionResult Index()
        {
           List<Category> objCategoryList = _dbContext.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj) {
        
            if(obj.Name.ToLower()==obj.DisplayOrder.ToString())
            
            if (ModelState.IsValid)
            {
                _dbContext.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
    }
}
