using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities
{
    /// <summary>
    /// RUBRO de cada Compañia
    /// </summary>
    public class SectorCompany
    {


        public int Id { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; } = null!;


        public ICollection<Company>? Companies { get; set; }


    }
}
