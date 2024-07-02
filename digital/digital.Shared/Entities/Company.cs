using digital.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace digital.Shared.Entities
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



        public City? City { get; set; }

        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CityId { get; set; }



        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un email válido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Email { get; set; } 


        //[Display(Name = "Código Postal")]
        //[MaxLength(10, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio.")]
        //public int PostalCode { get; set; }


        [Display(Name = "Página Web")]
        [Url(ErrorMessage = "El campo {0} debe ser una URL válida.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? WebPage { get; set; }



        //[Display(Name = "Forma Jurídica")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        //public LegalForms LegalForm { get; set; } = null!;
        //public int idLegalForm { get; set; }

        [Display(Name = "Forma Jurídica")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        [Required(ErrorMessage = "Debe seleccionar un valor para {1}.")]
        public LegalFormsEnum LegalForm { get; set; }



        //[Display(Name = "Rubro")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        //public SectorCompany Sector { get; set; } = null!;
        //public int idSector { get; set; }
        public SectorCompany Sector { get; set; } = null!;
        


        



        //[Display(Name = "Sector")]
        //[Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        //public int CodSector { get; set; }


        [Display(Name = "Tamaño de la Empresa")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        public SizeCompanyEnum CodSize { get; set; }



        [Display(Name = "Cantidad Empleados")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int QuantityEmployees { get; set; }



        [Display(Name = "Instalaciones Propias")]
        public bool OwnFacilities { get; set; }



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
        public float PorcExportacion { get; set; }



        [Display(Name = "Terciariza")]
        public bool Terciariza { get; set; }



        [Display(Name = "Observaciones")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string? Observaciones { get; set; } 



        public DateTime DateInsert { get; set; }
        public DateTime DateUpdate { get; set; } = DateTime.UtcNow;
        public DateTime? DateDelete { get; set; }



    }
}
