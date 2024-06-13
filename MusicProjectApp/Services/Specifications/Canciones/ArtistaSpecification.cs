namespace MusicProjectApp.Services.Specifications.Canciones
{
    public class ArtistaSpecification(int ArtistaId) : ICancionesSpecification
    {


        public bool IsValid(Models.Canciones element)
        {
            return element.ArtistaId == ArtistaId;

        }
    }
}
