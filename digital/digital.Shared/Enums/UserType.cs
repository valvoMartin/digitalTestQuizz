using System.ComponentModel;

namespace digital.Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin,

        [Description("Intermedio")]
        Intermediate,

        [Description("Usuario")]
        User,

    }
}
