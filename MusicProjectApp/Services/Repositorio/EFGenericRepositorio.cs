using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using MusicProjectApp.Models;

namespace MusicProjectApp.Services.Repositorio
{
    public class EFGenericRepositorio<T> : IGenericRepositorio<T> where T : class
    {
        private readonly GrupoAContext _context;

        public EFGenericRepositorio(GrupoAContext context)
        {
            _context = context;
        }

        public async Task<bool> Agregar(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Borrar(int id)
        {
            var entity = await DameUno(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<T>> DameTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> DameUno(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> Filtra(Expression<Func<T, bool>> predicado)
        {
            return await _context.Set<T>().Where(predicado).ToListAsync();
        }

        public async Task Modificar(int id, T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}