using System;
using System.IO;
using EcoTracer.Core.Data;
using EcoTracer.Core.Exceptions;
using EcoTracer.Core.Services;
using Microsoft.Data.Sqlite;
using Xunit;

namespace EcoTracer.Tests
{
    public class UsuarioServiceTests : IDisposable
    {
        private readonly string _caminhoDb;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            // Um arquivo de banco unico por teste, para um teste nunca enxergar dados de outro
            _caminhoDb = Path.Combine(Path.GetTempPath(), $"teste_usuarios_{Guid.NewGuid()}.db");

            var db = new AppDatabase(_caminhoDb);
            db.Inicializar();
            _service = new UsuarioService(db);
        }

        public void Dispose()
        {
            // Solta as conexoes que o SQLite mantem em cache; sem isso o Windows nao deixa apagar o arquivo
            SqliteConnection.ClearAllPools();
            if (File.Exists(_caminhoDb))
                File.Delete(_caminhoDb);
        }

        // ---------- Testes originais ----------

        [Fact]
        public void Cadastrar_DadosValidos_DeveCriarUsuario()
        {
            var usuario = _service.Cadastrar("Maycon Gomes", "maycon@teste.com", "senha123");

            Assert.True(usuario.Id > 0);
            Assert.Equal("Maycon Gomes", usuario.Nome);
            Assert.Equal("maycon@teste.com", usuario.Email);
        }

        [Fact]
        public void Cadastrar_EmailJaExistente_DeveLancarExcecao()
        {
            _service.Cadastrar("Primeiro", "duplicado@teste.com", "senha123");

            Assert.Throws<EmailJaCadastradoException>(() =>
                _service.Cadastrar("Segundo", "duplicado@teste.com", "outrasenha"));
        }

        [Fact]
        public void Autenticar_SenhaCorreta_DeveRetornarUsuario()
        {
            _service.Cadastrar("Maycon Gomes", "maycon2@teste.com", "senhaCorreta");

            var usuario = _service.Autenticar("maycon2@teste.com", "senhaCorreta");

            Assert.Equal("maycon2@teste.com", usuario.Email);
        }

        [Fact]
        public void Autenticar_SenhaIncorreta_DeveLancarExcecao()
        {
            _service.Cadastrar("Maycon Gomes", "maycon3@teste.com", "senhaCorreta");

            Assert.Throws<CredenciaisInvalidasException>(() =>
                _service.Autenticar("maycon3@teste.com", "senhaErrada"));
        }

        [Fact]
        public void Autenticar_EmailNaoCadastrado_DeveLancarExcecao()
        {
            Assert.Throws<CredenciaisInvalidasException>(() =>
                _service.Autenticar("naoexiste@teste.com", "qualquersenha"));
        }

        // ---------- Cadastro: entradas invalidas ----------

        [Theory]
        [InlineData("", "ana@teste.com", "senha123")]
        [InlineData("   ", "ana@teste.com", "senha123")]
        [InlineData("Ana", null, "senha123")]
        [InlineData("Ana", "ana@teste.com", "")]
        public void Cadastrar_CamposObrigatoriosVazios_DeveLancarExcecao(string nome, string? email, string senha)
        {
            Assert.Throws<EntradaInvalidaException>(() => _service.Cadastrar(nome, email!, senha));
        }

        [Fact]
        public void Cadastrar_EmailComCaixaDiferente_DeveLancarExcecao()
        {
            _service.Cadastrar("Ana", "Ana@Teste.com", "senha123");

            Assert.Throws<EmailJaCadastradoException>(() =>
                _service.Cadastrar("Outra Ana", "ANA@teste.com", "senha123"));
        }

        // ---------- Cadastro: comportamento ----------

        [Fact]
        public void Cadastrar_NomeEEmailComEspacosNasPontas_DeveRemoverEspacos()
        {
            var usuario = _service.Cadastrar("  Ana  ", "  espacos@teste.com  ", "senha123");

            Assert.Equal("Ana", usuario.Nome);
            Assert.Equal("espacos@teste.com", usuario.Email);
        }

        [Fact]
        public void Cadastrar_Senha_DeveSerGuardadaComHashENaoEmTextoPuro()
        {
            var usuario = _service.Cadastrar("Ana", "hash@teste.com", "senha123");

            Assert.NotEqual("senha123", usuario.SenhaHash);
            Assert.StartsWith("$2", usuario.SenhaHash);    // prefixo dos hashes BCrypt
            Assert.Equal(60, usuario.SenhaHash.Length);    // tamanho padrao de um hash BCrypt

            // O que esta gravado no banco tambem e o hash, nao a senha
            var doBanco = _service.Autenticar("hash@teste.com", "senha123");
            Assert.Equal(usuario.SenhaHash, doBanco.SenhaHash);
        }

        [Fact]
        public void Cadastrar_MesmaSenhaParaUsuariosDiferentes_DeveGerarHashesDiferentes()
        {
            // O BCrypt embute um "sal" aleatorio em cada hash
            var primeiro = _service.Cadastrar("Um", "um@teste.com", "senha123");
            var segundo = _service.Cadastrar("Dois", "dois@teste.com", "senha123");

            Assert.NotEqual(primeiro.SenhaHash, segundo.SenhaHash);
        }

        // ---------- Login ----------

        [Fact]
        public void Autenticar_EmailComCaixaDiferente_DeveRetornarUsuario()
        {
            _service.Cadastrar("Ana", "Ana@Teste.com", "senha123");

            var usuario = _service.Autenticar("ANA@teste.com", "senha123");

            Assert.Equal("Ana@Teste.com", usuario.Email);
        }

        [Fact]
        public void Autenticar_SenhaComCaixaDiferente_DeveLancarExcecao()
        {
            // A senha, diferente do e-mail, diferencia maiuscula de minuscula
            _service.Cadastrar("Ana", "caixa@teste.com", "Senha123");

            Assert.Throws<CredenciaisInvalidasException>(() =>
                _service.Autenticar("caixa@teste.com", "senha123"));
        }

        [Theory]
        [InlineData("", "senha123")]
        [InlineData(null, "senha123")]
        [InlineData("ana@teste.com", "")]
        public void Autenticar_CamposVazios_DeveLancarExcecao(string? email, string senha)
        {
            Assert.Throws<CredenciaisInvalidasException>(() => _service.Autenticar(email!, senha));
        }

        [Fact]
        public void Autenticar_EmailOuSenhaErrados_NaoDeveRevelarQualDosDoisEstaErrado()
        {
            _service.Cadastrar("Ana", "msg@teste.com", "senha123");

            var erroEmail = Assert.Throws<CredenciaisInvalidasException>(() =>
                _service.Autenticar("outro@teste.com", "senha123"));
            var erroSenha = Assert.Throws<CredenciaisInvalidasException>(() =>
                _service.Autenticar("msg@teste.com", "errada"));

            Assert.Equal(erroEmail.Message, erroSenha.Message);
        }

        [Fact]
        public void Autenticar_DadosCorretos_DeveRetornarMesmoIdENomeDoCadastro()
        {
            var cadastrado = _service.Cadastrar("Ana Souza", "dados@teste.com", "senha123");

            var autenticado = _service.Autenticar("dados@teste.com", "senha123");

            Assert.Equal(cadastrado.Id, autenticado.Id);
            Assert.Equal("Ana Souza", autenticado.Nome);
        }
    }
}