using Microsoft.EntityFrameworkCore;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LoansContext context) : base(context)
        {
        }

        public List<Loan> GetAll()
        {
            return dbSet.Include(x => x.Person).Include(y => y.Thing.Category).ToList();
        }

        public bool IsLoaned(int id)
        {
            var loan = dbSet.Where(x => x.ThingId == id && x.ReturnDate == null).SingleOrDefault();
            return loan != null;
        }
    }
}
