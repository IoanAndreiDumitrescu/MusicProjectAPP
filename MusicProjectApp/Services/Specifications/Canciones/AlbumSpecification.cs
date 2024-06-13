using MusicProjectApp.Models;

namespace MusicProjectApp.Services.Specifications.Canciones
{
    public class AlbumSpecification(int AlbumId) : ICancionesSpecification
    {
        public bool IsValid(Models.Canciones element)
        {
            return element.AlbumId == AlbumId;
        }
    }
}
