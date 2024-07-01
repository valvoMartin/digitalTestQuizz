using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.Entities
{
    public class Company
    {
       
        public int Id { get; set; }

        [Display(Name = "CUIL")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es obligatorio.")]
        public int Cuil { get; set; }


        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;


        [Display(Name = "Ubicacion")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CityId { get; set; }


        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un email válido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Email { get; set; }


        [Display(Name = "Código Postal")]
        [MaxLength(10, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PostalCode { get; set; }


        [Display(Name = "Página Web")]
        [Url(ErrorMessage = "El campo {0} debe ser una URL válida.")]
        public string? WebPage { get; set; }


        [Display(Name = "Código de Forma Jurídica")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CodFormaJuridica { get; set; }

        public int CodRubro { get; set; }

        public int CodTamaño { get; set; }
        public int CodSector { get; set; }
        public int CantidadEmpleados { get; set; }
        public bool InstalacionesPropias { get; set; }
        public int PorcAdmin { get; set; }
        public int PorcComercializacion { get; set; }
        public int PorcProduccion { get; set; }
        public int PorcRRHH { get; set; }
        public int PorcLogistica { get; set; }
        public int PorcMantenimiento { get; set; }
        public int PorcProductoDestinadoAMercadoLocal { get; set; }
        public int PorcExportacion { get; set; }
        public bool Terciariza { get; set; }

        public string? Observaciones { get; set; } 

        public DateTime DateDelete { get; set; }
        public DateTime DateInsert { get; set; } = DateTime.Now;
        public DateTime DateUpdate { get; set; } = DateTime.Now;



    }
}
