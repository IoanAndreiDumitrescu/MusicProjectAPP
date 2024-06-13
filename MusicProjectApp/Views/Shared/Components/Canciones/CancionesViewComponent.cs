using System.Collections;
using Microsoft.AspNetCore.Mvc;
using MusicProjectApp.Models;
using MusicProjectApp.Services.Repositorio;
using MusicProjectApp.Services.Specifications.Canciones;

namespace MusicProjectApp.Views.Shared.Components.Canciones
{
    public class CancionesViewComponent(IGenericRepositorio<Models.Canciones> coleccion,
        IGenericRepositorio<Models.Artistas> colArt) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ICancionesSpecification especificacion)
        {
            var coleccionInicial = await coleccion.DameTodos();
            if (especificacion is not null)
                coleccionInicial = coleccionInicial.Where(x=>especificacion.IsValid(x)).ToList();
            foreach (var elemento in coleccionInicial)
            {
                if (elemento is not null)  
                {
                    var Artista = await colArt.DameUno((int)elemento.ArtistaId);
                    if (Artista is not null)
                        elemento.Artista.Nombre = Artista.Nombre;
                }
            }
            return View(coleccionInicial);
        }
    }
}
