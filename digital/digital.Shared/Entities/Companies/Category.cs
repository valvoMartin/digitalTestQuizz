using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using digital.Shared.Entities.Countries;

namespace digital.Shared.Entities.Companies
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



        // FKs

        public Country Country { get; set; } = null!;

        [Display(Name = "Pais")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "Debe seleccionar un valor para {1}.")]
        public int CountryId { get; set; }



        public Sector Sector { get; set; } = null!;

        [Display(Name = "Sector")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "Debe seleccionar un valor para {1}.")]
        public int SectorId { get; set; }





        // Agrego la Relacion de Muchos
        public ICollection<Company>? Companies { get; set; }

    }
}
