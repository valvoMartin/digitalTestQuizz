using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities.Companies
{
    public class Rubro
    {
        public int Id { get; set; }


        [Display(Name = "Rubro")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;


        public ICollection<Sector>? Sectors { get; set; }

    }
}
