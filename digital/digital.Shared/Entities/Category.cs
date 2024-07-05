using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace digital.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de Categoria")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;


        [Display(Name = "Limite de Ganancia")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal? RevenueLimit { get; set; }


        [Display(Name = "Limite de Empleados")]
        [MaxLength(5, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int? EmployesLimit { get; set; }



        // FKs

        public Country Country { get; set; } = null!;
        public int CountryId { get; set; } 


        // Agrego la Relacion de Muchos
        public ICollection<Company>? Companies { get; set; }

    }
}
