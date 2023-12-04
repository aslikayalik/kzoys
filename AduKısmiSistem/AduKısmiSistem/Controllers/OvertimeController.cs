using AduKısmiSistem.Data;
using AduKısmiSistem.DTOs;
using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AduKısmiSistem.Controllers
{
    [Authorize]
    public class OvertimeController : Controller
    {
        private ApplicationDbContext _context;

        IStudentRepository<Student> _studentRepository;
        IOvertimeRepository<Overtime> _overtimeRepository;
        IWageRepository _wageRepository;
        private readonly UserManager<IdentityUser> _userManager;



        public OvertimeController(ApplicationDbContext context, IStudentRepository<Student> studentRepository, 
            IOvertimeRepository<Overtime> overtimeRepository, IWageRepository wageRepository,
            UserManager<IdentityUser> userManager )
        {
            _context = context;
            _studentRepository = studentRepository;
            _overtimeRepository = overtimeRepository;
            _wageRepository = wageRepository;
            _userManager = userManager;
       

        }



        public IActionResult Index()
        {
            List<Overtime> overtimes;
            List<Wage> wages;

            if (User.IsInRole("Admin"))
            {
                overtimes = _context.Overtime.Include(x => x.Student).ToList();
                wages = _wageRepository.GetAll();
            }
            else 
            {
                var currentUser = _context.ApplicationUser.FirstOrDefault(u => u.UserName == User.Identity.Name);

                if (currentUser != null)
                {
                    var userDepartmentId = currentUser.DepartmentId;

                    overtimes = _context.Overtime.Include(x => x.Student)
                        .Where(o => o.Student.DepartmentId == userDepartmentId)
                        .ToList();

                   
                    wages = _wageRepository.GetAll();
                }
                else
                {
                   
                    overtimes = new List<Overtime>(); 
                    wages = _wageRepository.GetAll();
                }
            }

            return View((overtimes, wages));
        }




        public IActionResult Create(int id)
        {
            List<StudentDto> students = _studentRepository.SelectStudentDto();
            return View((new Overtime { StudentId = id }, students));
        }




        [HttpPost]
        public IActionResult Create(int id, Overtime overtime)
        {
            if (ModelState.IsValid)
            {

                var student = _context.Student.FirstOrDefault(s => s.Id == id);


                if (student != null)
                {

                    overtime.Student = student;

                    _overtimeRepository.Add(overtime);
                    TempData[SD.Success] = "Mesai başarılı bir şekilde eklendi.";

                    return RedirectToAction("Create", "Overtime");

                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Geçersiz öğrenci ID.");
                }
            }


            return View(overtime);
        }

    


   

        [HttpGet]
        [Route("Overtime/GetStudentOvertimes/{id}")]
        public IActionResult GetStudentOvertimes(int id)
        {
            var overtimes = _context.Overtime
            .Where(o => o.StudentId == id)
            .Select(o => new
            {
                title = o.Title,
                start = o.StartTime.ToString("yyyy-MM-ddTHH:mm:ss")
            })
            .ToList();

            return Json(overtimes);
        }

  


    }
}
