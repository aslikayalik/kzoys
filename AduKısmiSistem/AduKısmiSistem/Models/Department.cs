using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AduKısmiSistem.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [DisplayName("Birim Adı")]
        public string DepartmentName { get; set; }

    
        public virtual ICollection<Student> Students { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
