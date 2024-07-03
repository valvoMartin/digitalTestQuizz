﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.Enums
{
    public enum SectorCompanyEnum
    {
        [Description("Actividades Artisticas, entrenimiento y recreativas")]
        Artistica = 1,

        [Description("Actividades de Alojamiento y servicio de Comidas")]
        Alojamiento,

        [Description("Actividades de atencion de Salud Humana y Asistencia Social")]
        AtencionHumana,

        [Description("Actividades de Servicios")]
        Servicios,

        [Description("Actividades de servicios Administrativos, de apoyo e Inmobiliarias")]
        Administrativo,

        [Description("Actividades Financieras y de Seguros")]
        Financiera,

        [Description("Actividades Profesionales, Cientificas y Tecnicas")]
        Profesionales,

        [Description("Agricultura, Ganaderia, Silvicultura y Pesca")]
        Agricultura,

        [Description("Construccion")]
        Construccion,

        [Description("Enseñanza")]
        Enseñanza,

        [Description("Industrias Manufacturera")]
        Industria,

        [Description("Industria Tecnologica")]
        Tecnologica,

        [Description("Informacion y Comunicacion")]
        Comunicacion,

        [Description("Suministros de Agua, Gas y Agua")]
        ServiciosImpuestos,

        [Description("Transporte y Almacenamiento")]
        Transporte,

        [Description("Otras Actividades")]
        Otras,
    }
}