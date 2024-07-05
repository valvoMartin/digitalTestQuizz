using digital.Shared.Entities.Companies;
using digital.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities.Countries
{
    public class Country : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "País")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;





        [Display(Name = "Provincias")]
        public int StatesNumber => States == null || States.Count == 0 ? 0 : States.Count;




        // FKs

        public ICollection<State>? States { get; set; }

        public ICollection<Category>? Categories { get; set; }

    }
}
