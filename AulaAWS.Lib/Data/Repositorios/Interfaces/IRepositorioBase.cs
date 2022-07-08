using AulaAWS.Lib.Models;

namespace AulaAWS.Lib.Data.Repositorios.Interfaces
{
    public interface IRepositorioBase<T> where T : ModelBase
    {
        public List<T> ListarTodos();
        public void Adicionar(T item);
        public void Deletar(int id);
    }
}