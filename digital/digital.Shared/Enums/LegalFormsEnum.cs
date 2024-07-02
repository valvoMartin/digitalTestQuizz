using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.Enums
{
    public enum LegalFormsEnum
    {
        [Description("Autonomo")]
        A = 1,

        [Description("Sociedad Civil 'S.C.'")]
        SC,

        [Description("Sociedad de Responsabilidad Limitada 'S.R.L.'")]
        SRL,

        [Description("Sociedad Anonima 'S.A.'")]
        SA,

        [Description("Sociedad Cooperativa 'S.Coop.'")]
        SCoop
    }
}
