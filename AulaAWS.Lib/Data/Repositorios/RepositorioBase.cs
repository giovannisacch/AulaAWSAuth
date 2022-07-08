using AulaAWS.Lib.Data.Repositorios.Interfaces;
using AulaAWS.Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace AulaAWS.Lib.Data.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : ModelBase
    {
        protected readonly AWSLoginContext _context;
        protected readonly DbSet<T> _dbset;

        public RepositorioBase(AWSLoginContext context, DbSet<T> dbset = null)
        {
            _context = context;
            _dbset = dbset;
        }

        public List<T> ListarTodos()
        {
            return _dbset.AsNoTracking().ToList();
        }
        public void Adicionar(T item)
        {
            _dbset.Add(item);
            _context.SaveChanges();
        }
        public void Deletar(int id)
        {
            var item = _dbset.Find(id);
            _dbset.Remove(item);
            _context.SaveChanges();
        }
    }
}