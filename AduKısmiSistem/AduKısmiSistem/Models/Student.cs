using AduKısmiSistem.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AduKısmiSistem.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Öğrenci Adı")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Öğrenci Soyadı")]
        public string StudentLastName { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("TC")]
        public string TC { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("IBAN")]
        public string IBAN { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("SGK Tipi")]
        public SGKType SGKType { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("SGK Giriş Tarihi")]
        public DateTime SGKEntrance { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("SGK Çıkış Tarihi")]
        public DateTime SGKExit { get; set; }

        [ForeignKey("Department")]
        [DisplayName("Birim")]
        [Required(ErrorMessage = "Zorunlu Alan")]
        public int DepartmentId { get; set; }

        // Relational Property 
        public virtual Department Department { get; set; }
        public ICollection<Overtime> Overtimes { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;




    }
}
