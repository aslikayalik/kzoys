using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AduKısmiSistem.Models
{
    public class Overtime
    {
        [Key]
        public int Id { get; set; }
    
        public int Title { get; set; }
      
        public DateTime StartTime { get; set; }
        [ForeignKey("Student")]
      
        public int StudentId { get; set; }
        // relational property 
        public Student Student { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
