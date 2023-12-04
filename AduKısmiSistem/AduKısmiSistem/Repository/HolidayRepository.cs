using AduKısmiSistem.Data;
using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using Microsoft.EntityFrameworkCore;

namespace AduKısmiSistem.Repository
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly ApplicationDbContext _context;
        public HolidayRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Holiday holiday)
        {
            _context.Add(holiday);
            return Save();
        }

        public bool Delete(Holiday holiday)
        {
            _context.Remove(holiday);
            return Save();
        }

        public  int GetTotalCount()
        {
            return _context.Holiday.Count(); 
        }


        public List<Holiday> GetAll(int page, int pageSize)
        {
          
            return _context.Holiday
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<Holiday> GetByIdAsync(int id)
        {
            return await _context.Holiday.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Holiday holiday)
        {
            _context.Update(holiday);
            return Save();
        }


    }
}
