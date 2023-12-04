using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AduKısmiSistem.Models
{
    public class Wage
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        public decimal HourlyMinumumWage { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public decimal HourlyWage { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
