using AduKısmiSistem.Models;

namespace AduKısmiSistem.Interfaces
{
    public interface IHolidayRepository
    {
      
        Task<Holiday> GetByIdAsync(int id);
        bool Add(Holiday holiday);
        bool Update(Holiday holiday);
        bool Delete(Holiday holiday);
        bool Save();
        int GetTotalCount();
        List<Holiday> GetAll(int page, int pageSize);
    }
}
