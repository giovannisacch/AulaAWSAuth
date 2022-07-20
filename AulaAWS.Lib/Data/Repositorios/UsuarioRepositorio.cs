using AulaAWS.Lib.Data.Repositorios.Interfaces;
using AulaAWS.Lib.Models;
using Microsoft.EntityFrameworkCore;

namespace AulaAWS.Lib.Data.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {

        public UsuarioRepositorio(AWSLoginContext context) : base(context, context.Usuarios)
        {

        }

        public async Task AlterarSenhaAsync(int id, string senha)
        {
            var usuario = await _dbset.FindAsync(id);
            usuario.SetSenha(senha);
            await _context.SaveChangesAsync();
        }
        public async Task AtualizarImagemAsync(int id, string nomeArquivo)
        {
            var usuario = await _dbset.FindAsync(id);
            usuario.SetUrlImagem(nomeArquivo);
            await _context.SaveChangesAsync();
        }
        public async Task<Usuario> BuscarUsuarioPorEmail(string email)
        {
            var usuario = await _dbset.AsNoTracking().FirstAsync(x => x.Email == email);
            if (usuario != null)
                return usuario;
            else
                throw new Exception("Nenhum usuario foi encontrado com esse email!");
        }
    }
}