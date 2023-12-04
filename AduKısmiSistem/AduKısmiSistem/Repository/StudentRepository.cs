using AduKısmiSistem.Data;
using AduKısmiSistem.DTOs;
using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AduKısmiSistem.Repository
{
    public class StudentRepository<T> : IStudentRepository<T> where T : Student
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> table;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
            table = context.Set<T>();
        }
        public bool Add(Student student)
        {
            _context.Add(student);
            return Save();
        }

        public bool Delete(Student student)
        {
            _context.Remove(student);
            return Save();
        }
    
        public  int GetTotalCount()
        {
            return  _context.Student.Count(); 
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            
            return await _context.Student.Include(s => s.Department).FirstOrDefaultAsync(i => i.Id == id);
        }
    

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Student student)
        {
            _context.Update(student);
            return Save();
        }
        public List<StudentDto> SelectStudentDto()
        {
            return table.Select(x =>
             new StudentDto()
             {
                 StudentName = x.StudentName,
                 StudentLastName = x.StudentLastName,
                 Id = x.Id
             }).ToList();
        }

   
    }
}
