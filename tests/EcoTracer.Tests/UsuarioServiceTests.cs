using EcoTracer.Core.Data;
using EcoTracer.Core.Exceptions;
using EcoTracer.Core.Services;
using Xunit;

namespace EcoTracer.Tests
{
    public class UsuarioServiceTests : IDisposable
    {
        private readonly string _caminhoDb;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            // Cria um nome de arquivo único para cada execução de teste, evitando conflito de bloqueio
            _caminhoDb = $"teste_usuarios_{Guid.NewGuid()}.db";

            var db = new AppDatabase(_caminhoDb);
            db.Inicializar();
            _service = new UsuarioService(db);
        }

        public void Dispose()
        {
            try
            {
                if (File.Exists(_caminhoDb))
                    File.Delete(_caminhoDb);
            }
            catch
            {
                
            }
        }

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
    }
}