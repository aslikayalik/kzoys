using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AduKısmiSistem.Models
{
    public class Holiday
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Resmi Tatil Adı")]
        public string HolidayName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Başlangıç Tarihi")]
        public DateTime HolidayStartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Bitiş Tarihi")]
        public DateTime HolidayEndDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
