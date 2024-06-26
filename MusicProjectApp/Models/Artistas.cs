namespace MusicProjectApp.Models
{
    public class Artistas
    {
        public int Id { get; set; }

        public required string Nombre { get; set; }

        public string? Genero { get; set; }

        public DateTime Fecha { get; set; }

        public virtual ICollection<Canciones> Canciones { get; set; } = [];

        public virtual ICollection<Festival> Festival { get; set; } = [];
    }
}
