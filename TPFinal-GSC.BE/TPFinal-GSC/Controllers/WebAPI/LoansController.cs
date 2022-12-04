using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Dto;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.Controllers.WebAPI
{
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public LoansController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateLoan([FromBody] LoanForCreationDto loanDto)
        {
            if (loanDto == null)
                return BadRequest();

            var person = uow.PersonRepository.GetById(loanDto.PersonId);
            if (person is null)
                return BadRequest("PersonId not exist");

            var thing = uow.ThingRepository.GetById(loanDto.ThingId);
            if (thing is null)
                return BadRequest("ThingId not exist");

            var isLoaned = uow.LoanRepository.IsLoaned(thing.Id);
            if (isLoaned)
            {
                return BadRequest("Thing is already loaned");
            }

            var loan = mapper.Map<Loan>(loanDto);
            uow.LoanRepository.Add(loan);
            uow.Complete();

            return Ok(mapper.Map<LoanDto>(loan));
        }

        [HttpGet]
        public List<Loan> GetLoans()
        {
            return uow.LoanRepository.GetAll();
        }
    }
}
