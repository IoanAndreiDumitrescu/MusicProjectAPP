using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<T>> DameTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> DameUno(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public async Task<bool> Borrar(int Id)
        {
            var elemento = await DameUno(Id);
            if (elemento != null) _context.Set<T>().Remove(elemento);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<T>> DameTodosPorCondicionConRelaciones(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().Where(filter);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T> DameUnoConRelaciones(int id, params Expression<Func<T, object>>[] includes)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            foreach (var property in includes)
            {
                _context.Entry(entity).Reference(property).Load();
            }

            return entity;
        }

        public async Task<bool> Agregar(T element)
        {
            await _context.Set<T>().AddAsync(element);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Modificar(int id, T element)
        {
            _context.Entry(element).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> Filtra(Expression<Func<T, bool>> predicado)
        {
            return await _context.Set<T>().Where(predicado).ToListAsync();
        }
    }
}
