namespace TPFinal_GSC.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        ILoanRepository LoanRepository { get; }
        IPersonRepository PersonRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IThingRepository ThingRepository { get; }
        int Complete();
    }
}
