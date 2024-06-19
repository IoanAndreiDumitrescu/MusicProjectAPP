namespace MusicProjectApp.Models;

public partial class Festival
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? ArtistaId { get; set; }

    public string? Ciudad { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFinal { get; set; }

    public virtual Artistas? Artista { get; set; }
}
