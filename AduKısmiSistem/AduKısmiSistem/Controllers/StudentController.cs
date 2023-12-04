using AduKısmiSistem.Data;
using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using AduKısmiSistem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AduKısmiSistem.Controllers
{

    public class StudentController : Controller
    {
       

        private readonly IStudentRepository<Student> _studentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private ApplicationDbContext _context;


        public StudentController(IStudentRepository<Student> studentRepository, IDepartmentRepository departmentRepository,  ApplicationDbContext context)
        {
           
            _studentRepository = studentRepository;
            _departmentRepository = departmentRepository;
            _context=context;
        
        }

       
        public  IActionResult Index(int page = 1, int pageSize = 5)
        {
            
           var totalCount =  _studentRepository.GetTotalCount();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            page = page < 1 ? 1 : (page > totalPages ? totalPages : page);

            List<Student> students;
            if (User.IsInRole("Admin"))
            {
                students = _context.Student
                    .Include(s => s.Department)
                    .OrderBy(s => s.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            else 
            {
                var currentUser = _context.ApplicationUser.FirstOrDefault(u => u.UserName == User.Identity.Name);

                if (currentUser != null)
                {
                    var userDepartmentId = currentUser.DepartmentId;

                    students = _context.Student
                        .Include(s => s.Department)
                        .Where(s => s.DepartmentId == userDepartmentId)
                        .OrderBy(s => s.Id)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                }
                else
                {
                    students = new List<Student>(); 
                }
            }

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(students);
        }



        [Authorize(Policy = "Admin")]
        public ActionResult Create()
        {
          
            var departments = _context.Department.ToList();

            SelectList departmentList = new SelectList(departments, "Id", "DepartmentName");

            ViewBag.DepartmentList = departmentList;

            return View();
        }

       [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
        

            if (!ValidateIban(student.IBAN))
            {
                ModelState.AddModelError("IBAN", "Geçersiz IBAN!"); 
                ViewBag.DepartmentList = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");
                return View(student); 
            }

            if (!IsValidTCKN(student.TC))
            {
                ModelState.AddModelError("TC", "Geçersiz TC Kimlik Numarası!");
                ViewBag.DepartmentList = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");
                return View(student);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.DepartmentList = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");

              
                TempData[SD.Error] = "Formda hatalar var, lütfen tekrar deneyin.";

                return View(student);
            }

            _studentRepository.Add(student);
            TempData[SD.Success] = "Öğrenci başarılı bir şekilde eklendi.";

            return RedirectToAction("Index");
        }





        [Authorize(Policy = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return View("Error"); 
            }

            ViewBag.DepartmentList = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");
            return View(student);
        }

      
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (!ValidateIban(student.IBAN))
            {
                ModelState.AddModelError("IBAN", "Geçersiz IBAN!");
                ViewBag.DepartmentList = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");
                return View(student);
            }

            if (!IsValidTCKN(student.TC))
            {
                ModelState.AddModelError("TC", "Geçersiz TC Kimlik Numarası!");
                ViewBag.DepartmentList = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");
                return View(student);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.DepartmentList = new SelectList(_context.Department.ToList(), "Id", "DepartmentName");


                TempData[SD.Error] = "Formda hatalar var, lütfen tekrar deneyin.";

                return View(student);
            }

            _studentRepository.Update(student);
            TempData[SD.Success] = "Öğrenci başarılı bir şekilde güncellendi.";

            return RedirectToAction("Index");
        
        }


        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var studentDetails = await _studentRepository.GetByIdAsync(id);
            if (studentDetails == null) return View("Error");
         
            return View(studentDetails);
        }

       
        [Authorize(Policy = "Admin")]

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentDetails = await _studentRepository.GetByIdAsync(id);
            if (studentDetails == null) return View("Error");
            _studentRepository.Delete(studentDetails);
            TempData[SD.Success] = "Öğrenci başarılı bir şekilde silindi.";

            return RedirectToAction("Index"); 
        }



        private bool IsValidTCKN(string tckn)
        {
            if (string.IsNullOrWhiteSpace(tckn) || tckn.Length != 11 || !tckn.All(char.IsDigit) || tckn[0] == '0')
            {
                return false;
            }

            int[] digits = tckn.Select(c => int.Parse(c.ToString())).ToArray();
            int oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int evenSum = digits[1] + digits[3] + digits[5] + digits[7];

            int tenthDigit = (oddSum * 7 - evenSum) % 10;
            int eleventhDigit = (oddSum + evenSum + digits[9]) % 10;

            return digits[9] == tenthDigit && digits[10] == eleventhDigit;
        }



        private static bool ValidateIban(string iban)
        {
            iban = iban.Replace(" ", "").ToUpper(); 

          
            if (string.IsNullOrEmpty(iban) || iban.Length < 2)
                return false;

            string countryCode = iban.Substring(0, 2);
            int ibanLength = iban.Length;

            
            var countryLengths = new Dictionary<string, int>
        {
           
            { "TR", 26 },
           
        };

            if (!countryLengths.ContainsKey(countryCode) || ibanLength != countryLengths[countryCode])
                return false;

           
            var regex = new Regex(@"^[A-Z0-9]{" + (ibanLength - 4) + @"}$");
            if (!regex.IsMatch(iban.Substring(4)))
                return false;

            //  Luhn algoritması
            string rearrangedIban = iban.Substring(4) + iban.Substring(0, 4);
            string numericIban = "";
            foreach (char c in rearrangedIban)
            {
                if (char.IsDigit(c))
                    numericIban += c;
                else
                    numericIban += (c - 55).ToString();
            }

            int remainder = 0;
            foreach (char c in numericIban)
                remainder = (10 * remainder + (c - '0')) % 97;

            return remainder == 1;
        }



    }
}
