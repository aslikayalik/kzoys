using AduKısmiSistem.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AduKısmiSistem.Interfaces
{
    public interface IWageRepository
    {
        List<Wage> GetAll();
       
        Task<Wage> GetByIdAsync(int id);
      
        bool Update(Wage wage);
      
        bool Save();
       
    }
}
