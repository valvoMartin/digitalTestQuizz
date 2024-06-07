namespace digital.Shared.Interfaces
{
    /// <summary>
    /// Entidad para aquellas clases que implementan la propiedad nombre para luego poder hacer un formulario generico para mostrar con blazor
    /// </summary>
    public interface IEntityWithName
    {
        public string Name { get; set; }
    }
}
