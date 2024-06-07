using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres.")]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }
        public Country? Country { get; set; }

        //Ver https://youtu.be/w_mw7qcrsqc
        public ICollection<City>? Cities { get; set; }

        [Display(Name = "Ciudades")]
        public int CitiesNumbers => Cities == null || Cities.Count == 0 ? 0 : Cities.Count;
    }
}
