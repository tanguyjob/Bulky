using BulkyWeb.Controllers.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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


           
                if (obj.Name != null &&obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name", "The DisplayOrder connot exactly match the Name");
                }

                if (obj.Name!= null && obj.Name.ToLower() == "test")
                {
                    ModelState.AddModelError("", "test is an invalid value");
                }
            
            if (ModelState.IsValid)
            {
                _dbContext.Add(obj);
                _dbContext.SaveChanges();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }



        public IActionResult Edit(int? id)  
        {
            if (id == null || id==0)
            {
                return NotFound();
            }
            Category categoryFromDb = _dbContext.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
                { 
                    return NotFound(); 
                }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
     
          
            if (ModelState.IsValid)
            {
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }



        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _dbContext.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            Category obj = _dbContext.Categories.Find(id);

            if(obj==null)
            { 
                  return NotFound(); 
            }
            _dbContext.Categories.Remove(obj);
            _dbContext.SaveChanges();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
         
        }
    }
}
