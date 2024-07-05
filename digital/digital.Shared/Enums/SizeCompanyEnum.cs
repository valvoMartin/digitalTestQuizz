using System.ComponentModel;

namespace digital.Shared.Enums
{
    public enum SizeCompanyEnum
    {
        [Description("De 2 a 9 empleados")]
        DosANueve = 1,

        [Description("De 10 a 19 empleados")]
        DiezADiecinueve ,

        [Description("De 20 a 39 empleados")]
        VeinteATreintaYNueve,

        [Description("De 40 a 89 empleados")]
        CuearentaAOchentayNueve,

        [Description("Mas de 90 empleados")]
        MasdeNoventa

    }
}
