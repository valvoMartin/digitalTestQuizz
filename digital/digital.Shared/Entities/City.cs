using digital.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities
{
    public class City : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres.")]
        public string Name { get; set; } = null!;


        public int StateId { get; set; }

        public State? State { get; set; }

        // public ICollection<User>? Users { get; set; }
    }
}
