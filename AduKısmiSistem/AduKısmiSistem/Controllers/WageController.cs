using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AduKısmiSistem.Controllers
{
    [Authorize(Policy = "Admin")]
    public class WageController : Controller
    {
        private readonly IWageRepository _wageRepository;

        public WageController(IWageRepository wageRepository)
        {

            _wageRepository = wageRepository;

        }
        public IActionResult Index()
        {
           List<Wage> wages =  _wageRepository.GetAll();
            return View(wages);
        }



       

        public async Task<ActionResult> Edit(int id)
        {
            var wage = await _wageRepository.GetByIdAsync(id);

            if (wage == null)
            {
                return View("Error");
            }


            return View(wage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Wage wage)
        {
            if (ModelState.IsValid)
            {
                _wageRepository.Update(wage);
                TempData[SD.Success] = "Ücretler başarılı bir şekilde güncellendi";
                return RedirectToAction("Index");
            }
            return View(wage);
        }

      

    }
}
