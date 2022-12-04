using System.ComponentModel.DataAnnotations.Schema;

namespace TPFinal_GSC.Entities
{
    public class Loan : EntityBase
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public Thing Thing { get; set; }
        public int ThingId { get; set; }

        [NotMapped]
        public string Status => ReturnDate is null? "Pending":"Returned";
    }
}
