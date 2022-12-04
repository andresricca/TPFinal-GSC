using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        public bool IsLoaned(int id);
    }
}
