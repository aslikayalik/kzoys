using AduKısmiSistem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AduKısmiSistem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

         public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Department> Department { get; set; }


        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Wage> Wage { get; set; }

        public DbSet<Overtime> Overtime { get; set; }
    }
}
