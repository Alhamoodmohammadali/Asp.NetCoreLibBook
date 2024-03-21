using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using AppStoreUtility;
using AppStoreDataAccess.Repository.IRepository;
using AppStoreModels;

namespace FammsStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> ObjCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(ObjCategoryList);
        }
        public IActionResult UpSert(int? id)
        {
            var category = new Category();
            if (id == null || id == 0)
            {
                return View(category);
            }
            else
            {
                category = _unitOfWork.Category.Get(u => u.Id == id);
                return View(category);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    _unitOfWork.Category.Update(category);

                }
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
