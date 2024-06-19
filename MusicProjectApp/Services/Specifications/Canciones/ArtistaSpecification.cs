namespace MusicProjectApp.Services.Specifications.Canciones
{
    public class ArtistaSpecification(int artistaId) : ICancionesSpecification
    {


        public bool IsValid(Models.Canciones element)
        {
            return element.ArtistaId == artistaId;

        }
    }
}
