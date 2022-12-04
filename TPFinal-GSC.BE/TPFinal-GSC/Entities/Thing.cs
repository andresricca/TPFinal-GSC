namespace TPFinal_GSC.Entities
{
    public class Thing : EntityBase
    {
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
