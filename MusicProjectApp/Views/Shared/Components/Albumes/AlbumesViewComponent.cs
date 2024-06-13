using Microsoft.AspNetCore.Mvc;
using MusicProjectApp.Services.Repositorio;

namespace MusicProjectApp.Views.Shared.Components.Albumes
{

    public class AlbumesViewComponent(IGenericRepositorio<Models.Albumes> repositorio) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string color)
        {
            IEnumerable<Models.Albumes> coleccionInicial = await repositorio.DameTodos();
            coleccionInicial = coleccionInicial.Where(x => x.Titulo != null && x.Genero != null && x.Fecha != null);
            return View(coleccionInicial);
        }
    }


}