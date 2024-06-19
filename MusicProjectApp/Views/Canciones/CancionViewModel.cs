using MusicProjectApp.Models;

namespace MusicProjectApp.Views.Canciones
{
    public class CancionViewModel
    {
        public string? Titulo { get; set; }

        public List<Models.Canciones>? Canciones { get; set; }

        public Albumes? Album { get; set; }

        public string GenerosDelAlbum
        {
            get { return Album?.Genero!; }
        }

        public Artistas? Artista { get; set; }
    }
}


