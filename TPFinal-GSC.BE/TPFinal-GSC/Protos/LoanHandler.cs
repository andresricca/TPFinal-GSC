using Grpc.Core;
using TPFinal_GSC.DataAccess.Interfaces;

namespace TPFinal_GSC.Protos
{
    public class LoanHandler : LoanService.LoanServiceBase
    {
        private readonly IUnitOfWork uow;

        public LoanHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public override Task<LoanResponse> LoanReturn(LoanRequest request, ServerCallContext context)
        {
            var loan = uow.LoanRepository.GetById(request.Id);
            if (loan is null)
                return Task.FromResult(new LoanResponse
                {
                    Message = "Loan not found"
                });

            if (loan.Status == "Returned")
                return Task.FromResult(new LoanResponse
                {
                    Message = "Loan has already been returned"
                });

            loan.ReturnDate = DateTime.UtcNow;
            uow.LoanRepository.Update(loan);
            uow.Complete();

            return Task.FromResult(new LoanResponse
            {
                Message = "Loan returned successfully"
            });
        }
    }
}
