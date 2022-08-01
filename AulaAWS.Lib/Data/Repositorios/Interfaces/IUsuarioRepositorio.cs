using AulaAWS.Lib.Models;

namespace AulaAWS.Lib.Data.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
    {
        public Task AlterarSenhaAsync(Guid id, string senha);
        public Task AtualizarImagemAsync(Guid id, string nomeArquivo);
        public Task<Usuario> BuscarUsuarioPorEmail(string email);
    }
}