using Microsoft.EntityFrameworkCore;
using TPFinal_GSC.Entities;
using TPFinal_GSC.Models;

namespace TPFinal_GSC.DataAccess
{
    public class LoansContext : DbContext
    {
        public LoansContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Thing> Things { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
