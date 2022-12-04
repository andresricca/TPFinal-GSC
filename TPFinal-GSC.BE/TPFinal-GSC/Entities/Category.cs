namespace TPFinal_GSC.Entities
{
    public class Category : EntityBase
    {
        public string Description { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
