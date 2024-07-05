using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.Enums
{
    public enum RubroCompanyEnum
    {
        [Description("Agropecuario")]
        Agro = 1,

        [Description("Industria y Mineria")]
        IndsutriaMineria,

        [Description("Servicios")]
        Servicios,

        [Description("Construccion")]
        Construccion,

        [Description("Comercio")]
        Comercio,

       
    }
}
