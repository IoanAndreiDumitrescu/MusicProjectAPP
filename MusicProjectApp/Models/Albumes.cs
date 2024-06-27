namespace MusicProjectApp.Models
{
    public class Albumes
    {
        public int Id { get; init; }

        public string? Genero { get; init; }

        public DateTime Fecha { get; init; }

        public required string? Titulo { get; init; }

        public virtual ICollection<Canciones> Canciones { get; init; } = [];
    }
}
