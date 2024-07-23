using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities.Test
{
    public class SubArea
    {
        public int Id { get; set; }


        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;



        // Clave foránea para la relación con Area
        public int AreaId { get; set; }
        public Area? Area { get; set; }



        // Relación uno a muchos con Question
        public ICollection<Question>? Questions { get; set; }
    }
}
