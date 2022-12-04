using System.ComponentModel.DataAnnotations;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.Models
{
    public class ThingViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Categoría")]
        public Category? Category { get; set; }
        [Required(ErrorMessage = "La categoria es obligatoria.")]
        [Display(Name = "Categoría")]
        public int CategoryId { get; set; }
    }
}
