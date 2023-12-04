using AduKısmiSistem.Models;
using System.Linq.Expressions;

namespace AduKısmiSistem.Interfaces
{
    public interface IOvertimeRepository<T> where T : Overtime
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);

        T Default(Expression<Func<T, bool>> exp);

        bool Any(Expression<Func<T, bool>> exp);

    }
}
