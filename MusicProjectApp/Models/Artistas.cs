namespace MusicProjectApp.Models;

public partial class Artistas
{
    public int Id { get; set; }

    public required string Nombre { get; set; }

    public string? Genero { get; set; }

    public DateOnly? Fecha { get; set; }

    public virtual ICollection<Canciones> Canciones { get; set; } = [];

    public virtual ICollection<Festival> Festival { get; set; } = [];
}
