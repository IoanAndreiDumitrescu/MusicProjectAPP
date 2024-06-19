namespace MusicProjectApp.Models;

public partial class Albumes
{
    public int Id { get; set; }

    public string? Genero { get; set; }

    public DateOnly? Fecha { get; set; }

    public required string Titulo { get; set; }

    public virtual ICollection<Canciones> Canciones { get; set; } = [];
}
