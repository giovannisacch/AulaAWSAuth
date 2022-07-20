namespace AulaAWS.Web.DTOs
{
    public class UsuarioDTO : ModelBaseDTO
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}