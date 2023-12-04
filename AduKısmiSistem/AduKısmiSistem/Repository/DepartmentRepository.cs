using AduKısmiSistem.Data;
using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using Microsoft.EntityFrameworkCore;

namespace AduKısmiSistem.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool Delete(Department department)
        {
            _context.Remove(department);
            return Save();
        }
       
        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Department.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Department department)
        {
            _context.Update(department);
            return Save();
        }

    
        public int GetTotalCount()
        {
            return _context.Department.Count();
        }
        public List<Department> GetAll(int page, int pageSize)
        {
            
            return  _context.Department
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

    }
}
