using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MusicProjectApp.Models;

namespace MusicProjectApp.Services.Repositorio
{
    public class EfGenericRepositorio<T>(GrupoAContext context) : IGenericRepositorio<T>
        where T : class
    {
        public async Task<bool> Agregar(T element)
        {
            await context.Set<T>().AddAsync(element);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Borrar(int id)
        {
            var entity = await DameUno(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<T>> DameTodos()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> DameUno(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> Filtra(Expression<Func<T, bool>> predicado)
        {
            return await context.Set<T>().Where(predicado).ToListAsync();
        }

        public async Task Modificar(int id, T element)
        {
            context.Set<T>().Update(element);
            await context.SaveChangesAsync();
        }
    }
}