using TPFinal_GSC.DataAccess.Interfaces;

namespace TPFinal_GSC.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LoansContext context;
        public ILoanRepository LoanRepository { get; private set; }
        public IPersonRepository PersonRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IThingRepository ThingRepository { get; private set; }

        public UnitOfWork(LoansContext context)
        {
            this.context = context;
            LoanRepository = new LoanRepository(context);
            PersonRepository = new PersonRepository(context);
            CategoryRepository = new CategoryRepository(context);
            ThingRepository = new ThingRepository(context);
        }
        public int Complete()
        {
            return context.SaveChanges();
        }
    }
}
