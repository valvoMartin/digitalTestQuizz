using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.Enums
{
    public enum SectorCompanyEnum
    {
       
        [Description("Agricultura, Ganaderia, Silvicultura y Pesca")]
        AgropecuarioA = 1,




        [Description("Explotacion de minas y Canteras")]
        IndustriaMineriaA,

        [Description("Transporte y Almacenamiento")]
        IndustriaMineriaB,

        [Description("Informacion y Comunicacion")]
        IndustriaMineriaC,




        [Description("Electricidad, Gas, Vapor y Aire acondicionado")]
        ServiciosA,

        [Description("Suministros de Agua, Cloacas, Gestio de residuos y Recuperacion de Materiales")]
        ServiciosB,

        [Description("Servicio de Transporte y Almacenamiento(No industria y Mineria)")]
        ServiciosC,

        [Description("Servicio de Alojamiento y Servicio de Comidas")]
        ServiciosD,

        [Description("Intermediacion Financiera y servicios de Seguros")]
        ServiciosE,

        [Description("Servicios Inmobiliarios")]
        ServiciosF,

        [Description("Actividades Profesionales, Cientificas y Tecnicas")]
        ServiciosG,

        [Description("ACTIVIDADES ADMINISTRATIVAS Y SERVICIOS DE APOYO")]
        ServiciosH,

        [Description("ENSEÑANZA")]
        ServiciosI,

        [Description("SALUD HUMANA Y SERVICIOS SOCIALES")]
        ServiciosJ,

        [Description("SERVICIOS ARTÍSTICOS, CULTURALES, DEPORTIVOS Y DE ESPARCIMIENTO")]
        ServiciosK,

        [Description("SERVICIOS DE ASOCIACIONES Y SERVICIOS PERSONALES")]
        ServiciosL,




        [Description("CONSTRUCCIÓN")]
        ConstruccionA,



        [Description("COMERCIO AL POR MAYO Y AL POR MENOR, REPARACIÓN DE VEHÍCULOS AUTOMOTORES Y MOTOCICLETAS")]
        ComercioA,

        

    }
}
