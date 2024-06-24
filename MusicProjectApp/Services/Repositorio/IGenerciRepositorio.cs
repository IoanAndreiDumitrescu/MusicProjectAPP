﻿using System.Linq.Expressions;

namespace MusicProjectApp.Services.Repositorio
{
    public interface IGenericRepositorio<T> where T : class
    {
        Task<List<T>> DameTodos();
        Task<T?> DameUno(int id);
        Task<bool> Borrar(int id);
        Task<bool> Agregar(T element);
        Task Modificar(int id, T element);
        Task<List<T>> Filtra(Expression<Func<T, bool>> predicado);
        
    }
}