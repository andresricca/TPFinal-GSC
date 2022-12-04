namespace TPFinal_GSC.Dto
{
    public class LoanDto
    {
        public DateTime Date { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int PersonId { get; set; }
        public int ThingId { get; set; }
    }
}
