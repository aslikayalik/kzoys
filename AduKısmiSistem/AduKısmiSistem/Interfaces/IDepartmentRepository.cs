using AduKısmiSistem.Models;

namespace AduKısmiSistem.Interfaces
{
    public interface IDepartmentRepository
    {
      
        Task<Department> GetByIdAsync(int id);
        bool Add(Department department);
        bool Update(Department department);
        bool Delete(Department department);
        bool Save();
      
        int GetTotalCount();
        List<Department> GetAll(int page, int pageSize);
    }
}
