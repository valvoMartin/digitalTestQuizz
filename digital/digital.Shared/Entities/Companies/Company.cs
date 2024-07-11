using digital.Shared.Entities.Countries;
using digital.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace digital.Shared.Entities.Companies
{
    public class Company
    {

        public int Id { get; set; }



        [Display(Name = "CUIT")]
        [MaxLength(11, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Cuit { get; set; } = null!;



        [Display(Name = "Razon Social")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;




        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un email válido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Email { get; set; }





        [Display(Name = "Página Web")]
        [Url(ErrorMessage = "El campo {0} debe ser una URL válida.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? WebPage { get; set; }





        [Display(Name = "Forma Jurídica")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        [Required(ErrorMessage = "Debe seleccionar un valor para {1}.")]
        public LegalFormsEnum LegalForm { get; set; }



        



        [Display(Name = "Tamaño de la Empresa")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "Debe seleccionar un {1}.")]
        public SizeCompanyEnum Size { get; set; }

       



        [Display(Name = "Porcentaje de Administración")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe ser un porcentaje válido.")]
        public float PorcAdministracion { get; set; }




        [Display(Name = "Porcentaje de Comercialización")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe ser un porcentaje válido.")]
        public float PorcComercializacion { get; set; }




        [Display(Name = "Porcentaje de Producción")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe ser un porcentaje válido.")]
        public float PorcProduccion { get; set; }




        [Display(Name = "Porcentaje de RRHH")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe ser un porcentaje válido.")]
        public float PorcRRHH { get; set; }




        [Display(Name = "Porcentaje de Logística")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe ser un porcentaje válido.")]
        public float PorcLogistica { get; set; }




        [Display(Name = "Porcentaje de Mantenimiento")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe ser un porcentaje válido.")]
        public float PorcMantenimiento { get; set; }




        [Display(Name = "Porcentaje de Producto Destinado al Mercado Local")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe ser un porcentaje válido.")]
        public float PorcProductoDestinadoAMercadoLocal { get; set; }




        [Display(Name = "Porcentaje de Producto Destinado a Exportación")]
        [Range(0, 100, ErrorMessage = "El campo {0} debe ser un porcentaje válido.")]
        public float PorcProductoDestinadoAMercadoExterior { get; set; }




        [Display(Name = "Terciariza")]
        public bool Terciariza { get; set; }



        [Display(Name = "Instalaciones Propias")]
        public bool OwnFacilities { get; set; }



        [Display(Name = "Observaciones")]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Observaciones { get; set; }




        public DateTime DateInsert { get; set; }
        public DateTime DateUpdate { get; set; } = DateTime.UtcNow;
        public DateTime? DateDelete { get; set; }





        // FKs

        public Category Category { get; set; } = null!;

        [Display(Name = "Categoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CategoryId { get; set; }




        public City City { get; set; } = null!;

        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CityId { get; set; }



        public Sector Sector { get; set; } = null!;

        [Display(Name = "Sector")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Required(ErrorMessage = "Debe seleccionar un valor para {1}.")]
        public int SectorId { get; set; }



        // public ICollection<User> Users { get; set; }


    }
}
