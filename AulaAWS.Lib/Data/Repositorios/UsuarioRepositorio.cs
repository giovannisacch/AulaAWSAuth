using AulaAWS.Lib.Data.Repositorios.Interfaces;
using AulaAWS.Lib.Models;

namespace AulaAWS.Lib.Data.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        private readonly AWSLoginContext _context;
        public UsuarioRepositorio(AWSLoginContext context) : base(context, context.Usuarios)
        {
            _context = context;
        }

        public void AlterarSenha(int id, string senha)
        {
            var usuario = _dbset.Find(id);
            usuario.SetSenha(senha);
            _context.SaveChanges();
        }
    }
}