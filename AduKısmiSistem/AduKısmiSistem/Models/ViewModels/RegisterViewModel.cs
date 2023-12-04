using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AduKısmiSistem.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Zorunlu Alan")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [StringLength(100, ErrorMessage = "Şifre en az 6 karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Oluşturduğunuz şifreniz ile eşleşmedi, tekrar giriniz lütfen.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
        public string RoleSelected { get; set; }
    }
}
