using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities.Companies
{
    public class Sector
    {
        public int Id { get; set; }



        [Display(Name = "Sector")]
        [MaxLength(95, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;



        public Rubro? Rubro { get; set; }

        [Display(Name = "Rubro")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "Debe seleccionar un valor para {1}.")]
        public int? RubroId { get; set; }




        public ICollection<Company> Companies { get; set; } = null!;

        public ICollection<Category> Categories { get; set; } = null!;
    }
}
