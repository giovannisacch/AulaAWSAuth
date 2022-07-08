namespace AulaAWS.Lib.Models
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string UrlImagemCadastro { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public Usuario(int id, string nome, string cpf, DateTime dataNascimento, string email, string senha)
        {
            SetId(id);
            SetNome(nome);
            SetCpf(cpf);
            SetDataNascimento(dataNascimento);
            SetEmail(email);
            SetSenha(senha);
            DataCriacao = DateTime.Now;
        }
        public bool ValidarSeDataNascimentoAnterior2010(DateTime dataNascimento)
        {
            if (dataNascimento < DateTime.Parse("01/01/2010"))
                return true;
            throw new Exception();
        }
        public bool ValidarSeEmailValido(string email)
        {
            if (email.Contains("@"))
                return true;
            throw new Exception();
        }
        public bool ValidarSeCpfPossuiOnzeDigitosEApenasNumeros(string cpf)
        {
            if ((cpf.Count() <= 11) & (!int.TryParse(cpf, out int value)))
                return true;
            throw new Exception();
        }
        public bool ValidarSeSenhaPossuiPeloMenosOitoDigitos(string senha)
        {
            if (senha.Count() > 8)
                return true;
            throw new Exception();
        }
        public void SetId(int id)
        {
            Id = id;
        }
        public void SetNome(string nome)
        {
            Nome = nome;
        }
        public void SetCpf(string cpf)
        {
            ValidarSeCpfPossuiOnzeDigitosEApenasNumeros(cpf);
            Cpf = cpf;
        }
        public void SetDataNascimento(DateTime dataNascimento)
        {
            ValidarSeDataNascimentoAnterior2010(dataNascimento);
            DataNascimento = dataNascimento;
        }
        public void SetEmail(string email)
        {
            ValidarSeEmailValido(email);
            Email = email;
        }
        public void SetSenha(string senha)
        {
            Senha = senha;
        }
        public void SetUrlImagem(string urlImagem)
        {
            UrlImagemCadastro = urlImagem;
        }
    }
}