using AduKısmiSistem.Data;
using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AduKısmiSistem.Repository
{
    public class WageRepository : IWageRepository
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<Wage> table;
        public WageRepository(ApplicationDbContext context)
        {
            _context = context;
            table = context.Set<Wage>();
        }

      

        public List<Wage> GetAll()
        {
            return table.ToList();
        }

        public async Task<Wage> GetByIdAsync(int id)
        {
            return await _context.Wage.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Wage wage)
        {
            _context.Update(wage);
            return Save();
        }
    }
}
