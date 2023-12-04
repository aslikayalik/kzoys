using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using AduKısmiSistem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AduKısmiSistem.Controllers
{
    [Authorize(Policy = "Admin")]
    public class HolidayController : Controller
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidayController(IHolidayRepository holidayRepository)
        {

            _holidayRepository = holidayRepository;

        }
        public IActionResult Index(int page = 1, int pageSize = 7)
        {
           
            var totalCount = _holidayRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

           
            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);

            var holidays = _holidayRepository.GetAll(page, pageSize);

           
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(holidays);
        }



        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Holiday holiday)
        {
            if (!ModelState.IsValid)
            {
                return View(holiday);

            }
            _holidayRepository.Add(holiday);
            TempData[SD.Success] = "Resmi tatil başarılı bir şekilde eklendi.";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var holiday = await _holidayRepository.GetByIdAsync(id);

            if (holiday == null)
            {
                return View("Error");
            }


            return View(holiday);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                _holidayRepository.Update(holiday);
                TempData[SD.Success] = "Resmi tatil başarılı bir şekilde güncellendi.";
                return RedirectToAction("Index");
            }
            return View(holiday);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var holidayDetails = await _holidayRepository.GetByIdAsync(id);
            if (holidayDetails == null) return View("Error");

            return View(holidayDetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var holidayDetails = await _holidayRepository.GetByIdAsync(id);
            if (holidayDetails == null) return View("Error");
            _holidayRepository.Delete(holidayDetails);
            TempData[SD.Success] = "Resmi tatil başarılı bir şekilde silindi.";

            return RedirectToAction("Index");
        }
    }
}
