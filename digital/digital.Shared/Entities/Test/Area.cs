using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities.Test
{
    public class Area
    {
        public int Id { get; set; }


        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;



        // Relación uno a muchos con SubArea
        public ICollection<SubArea>? SubAreas { get; set; }
    }
}
