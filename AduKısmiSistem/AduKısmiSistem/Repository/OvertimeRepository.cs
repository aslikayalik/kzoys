using AduKısmiSistem.Data;
using AduKısmiSistem.Interfaces;
using AduKısmiSistem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AduKısmiSistem.Repository
{
    public class OvertimeRepository<T> : IOvertimeRepository<T> where T : Overtime
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> table;

        public OvertimeRepository(ApplicationDbContext context)
        {
            _context = context;
            table = context.Set<T>();
        }
   

        private void Save()
        {
            _context.SaveChanges();
        }

        public void Add(T item)
        {
            table.Add(item);
            Save();
        }

        public void Delete(int id)
        {
            T item = GetById(id);
            table.Remove(item);
            Save();
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }



        public T GetById(int id)
        {
            return table.Find(id);
        }

  

        public void Update(T item)
        {
  
            table.Update(item);
            Save();
        }

        public T Default(Expression<Func<T, bool>> exp)
        {
            return table.FirstOrDefault(exp);
        }
        public bool Any(Expression<Func<T, bool>> exp)
        {
            return table.Any(exp);
        }
    }
}
