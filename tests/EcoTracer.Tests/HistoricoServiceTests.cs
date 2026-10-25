using System;
using System.IO;
using EcoTracer.Core.Data;
using EcoTracer.Core.Models;
using EcoTracer.Core.Services;
using Microsoft.Data.Sqlite;
using Xunit;

namespace EcoTracer.Tests
{
    public class HistoricoServiceTests : IDisposable
    {
        private readonly string _caminhoDb;
        private readonly HistoricoService _historicoService;
        private readonly UsuarioService _usuarioService;

        public HistoricoServiceTests()
        {
            _caminhoDb = Path.Combine(Path.GetTempPath(), $"teste_historico_{Guid.NewGuid()}.db");

            var db = new AppDatabase(_caminhoDb);
            db.Inicializar();
            _historicoService = new HistoricoService(db);
            _usuarioService = new UsuarioService(db);
        }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            if (File.Exists(_caminhoDb))
                File.Delete(_caminhoDb);
        }

        [Fact]
        public void Salvar_CalculoValido_DeveApareceNoHistorico()
        {
            var usuario = _usuarioService.Cadastrar("Teste", "historico1@teste.com", "senha123");
            var resultado = new ResultadoImpacto
            {
                Co2DiarioKg = 3.5,
                Co2AnualKg = 910.0,
                ArvoresNecessariasAnual = 112
            };

            _historicoService.Salvar(usuario.Id, 20, ModoTransporte.CarroGasolina, 5, resultado);

            var lista = _historicoService.ListarPorUsuario(usuario.Id);

            Assert.Single(lista);
            Assert.Equal(112, lista[0].ArvoresNecessariasAnual);
        }

        [Fact]
        public void ListarPorUsuario_SemCalculos_DeveRetornarListaVazia()
        {
            var usuario = _usuarioService.Cadastrar("Teste", "historico2@teste.com", "senha123");

            var lista = _historicoService.ListarPorUsuario(usuario.Id);

            Assert.Empty(lista);
        }

        [Fact]
        public void ListarPorUsuario_NaoDeveMisturarComOutroUsuario()
        {
            var usuario1 = _usuarioService.Cadastrar("Usuario Um", "um@teste.com", "senha123");
            var usuario2 = _usuarioService.Cadastrar("Usuario Dois", "dois@teste.com", "senha123");

            var resultado = new ResultadoImpacto { Co2DiarioKg = 1, Co2AnualKg = 100, ArvoresNecessariasAnual = 10 };
            _historicoService.Salvar(usuario1.Id, 10, ModoTransporte.Moto, 3, resultado);

            var listaUsuario2 = _historicoService.ListarPorUsuario(usuario2.Id);

            Assert.Empty(listaUsuario2);
        }
    }
}