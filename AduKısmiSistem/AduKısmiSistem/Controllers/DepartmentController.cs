using AduKısmiSistem.Data;
using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using AduKısmiSistem.Repository;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;


namespace AduKısmiSistem.Controllers
{
   
    [Authorize(Policy = "Admin")]
    public class DepartmentController : Controller
    {
       
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController( IDepartmentRepository departmentRepository)
        {
           
            _departmentRepository = departmentRepository;

        }
       

        public IActionResult Index(int page = 1, int pageSize = 7)
        {

            var totalCount =  _departmentRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);


            var departments =  _departmentRepository.GetAll(page, pageSize);


            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(departments);
        }

        public IActionResult Create()
        {
      
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
              
            }
            _departmentRepository.Add(department);
            TempData[SD.Success] = "Birim başarıyla eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                return View("Error"); 
            }

          
            return View(department);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Update(department);
                TempData[SD.Success] = "Birim başarıyla güncellendi.";
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var departmentDetails = await _departmentRepository.GetByIdAsync(id);
            if (departmentDetails == null) return View("Error");

            return View(departmentDetails);
        }

          [HttpPost, ActionName("Delete")]
     
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departmentDetails = await _departmentRepository.GetByIdAsync(id);
            if (departmentDetails == null) return View("Error");
            _departmentRepository.Delete(departmentDetails);
            TempData[SD.Success] = "Birim başarıyla silindi.";

            return RedirectToAction("Index");
        }


       
    }
}
