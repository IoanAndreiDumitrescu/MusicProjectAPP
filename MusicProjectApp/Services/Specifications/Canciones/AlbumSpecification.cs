namespace MusicProjectApp.Services.Specifications.Canciones
{
    public class AlbumSpecification(int albumId) : ICancionesSpecification
    {
        public bool IsValid(Models.Canciones element)
        {
            return element.AlbumId == albumId;
        }
    }
}
