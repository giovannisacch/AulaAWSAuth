using Xunit;
using AulaAWS.Lib.Models;
using System;

namespace AulaAWS.Test;

public class UsuarioTest
{
    [Fact]
    public void TestandoSetId()
    {
        var valorEsperado = 1;
        var usuario = CriarUsuarioPadrao();

        usuario.SetId(valorEsperado);

        Assert.Equal(valorEsperado, usuario.Id);
    }
    [Fact]
    public void TestandoSetNome()
    {
        var valorEsperado = "Joao";
        var usuario = CriarUsuarioPadrao();

        usuario.SetNome(valorEsperado);

        Assert.Equal(valorEsperado, usuario.Nome);
    }
    [Fact]
    public void TestandoSetCpf()
    {
        var valorEsperado = "66655544454";
        var usuario = CriarUsuarioPadrao();

        usuario.SetCpf(valorEsperado);

        Assert.Equal(valorEsperado, usuario.Cpf);
    }
    [Fact]
    public void TestandoSetDataNascimento()
    {
        var valorEsperado = DateTime.Parse("29/04/1998");
        var usuario = CriarUsuarioPadrao();

        usuario.SetDataNascimento(valorEsperado);

        Assert.Equal(valorEsperado, usuario.DataNascimento);
    }
    [Fact]
    public void TestandoSetEmail()
    {
        var valorEsperado = "novoemail@email.com";
        var usuario = CriarUsuarioPadrao();

        usuario.SetEmail(valorEsperado);

        Assert.Equal(valorEsperado, usuario.Email);
    }
    [Fact]
    public void TestandoSetSenha()
    {
        var valorEsperado = "novasenha";
        var usuario = CriarUsuarioPadrao();

        usuario.SetSenha(valorEsperado);

        Assert.Equal(valorEsperado, usuario.Senha);
    }
    public Usuario CriarUsuarioPadrao()
    {
        return new Usuario(0, "Luiz", "44455566645", DateTime.Parse("30/04/1998"), "email@email.com", "senhaforte");
    }
}
