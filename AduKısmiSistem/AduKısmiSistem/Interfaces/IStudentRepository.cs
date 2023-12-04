using AduKısmiSistem.DTOs;
using AduKısmiSistem.Models;

namespace AduKısmiSistem.Interfaces
{
    public interface IStudentRepository<T> where T : Student
    {
        List<StudentDto> SelectStudentDto();
   
        Task<Student> GetByIdAsync(int id);
        bool Add(Student student);
        bool Update(Student student);
        bool Delete(Student student);
        bool Save();
        int GetTotalCount();
      
    }
}
