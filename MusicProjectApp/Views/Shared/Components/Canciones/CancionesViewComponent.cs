using Microsoft.AspNetCore.Mvc;
using MusicProjectApp.Services.Repositorio;
using MusicProjectApp.Services.Specifications.Canciones;

namespace MusicProjectApp.Views.Shared.Components.Canciones
{
    public class CancionesViewComponent(IGenericRepositorio<Models.Canciones> coleccion,
        IGenericRepositorio<Models.Artistas> colArt) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ICancionesSpecification? especificacion)
        {
            var coleccionInicial = await coleccion.DameTodos();
            if (especificacion is not null)
                coleccionInicial = coleccionInicial.Where(x=>especificacion.IsValid(x)).ToList();
            foreach (var elemento in coleccionInicial)
            {
                var artista = await colArt.DameUno((int)elemento.ArtistaId!);
                if (elemento.Artista != null) elemento.Artista.Nombre = artista!.Nombre;
            }
            return View(coleccionInicial);
        }
    }
}
