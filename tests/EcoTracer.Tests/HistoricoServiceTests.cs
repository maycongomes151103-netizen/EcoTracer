using System;
using System.IO;
using System.Threading;
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

        // ---------- Testes originais ----------

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

        // ---------- Novos ----------

        [Fact]
        public void Salvar_DeveGuardarTodosOsCamposDoCalculo()
        {
            var usuario = _usuarioService.Cadastrar("Teste", "campos@teste.com", "senha123");
            var resultado = new ResultadoImpacto { Co2DiarioKg = 0.52, Co2AnualKg = 135.2, ArvoresNecessariasAnual = 17 };

            _historicoService.Salvar(usuario.Id, 12.5, ModoTransporte.Moto, 3, resultado);

            var item = Assert.Single(_historicoService.ListarPorUsuario(usuario.Id));
            Assert.True(item.Id > 0);
            Assert.Equal(usuario.Id, item.UsuarioId);
            Assert.Equal(12.5, item.DistanciaDiariaKm);
            Assert.Equal(ModoTransporte.Moto, item.Modo);
            Assert.Equal(3, item.DiasPorSemana);
            Assert.Equal(135.2, item.Co2AnualKg, 2);
            Assert.Equal(17, item.ArvoresNecessariasAnual);
            Assert.InRange(item.DataCalculo, DateTime.Now.AddMinutes(-1), DateTime.Now.AddMinutes(1));
        }

        [Fact]
        public void Salvar_VariosCalculos_DeveListarTodosDoUsuario()
        {
            var usuario = _usuarioService.Cadastrar("Teste", "varios@teste.com", "senha123");
            var resultado = new ResultadoImpacto { Co2DiarioKg = 1, Co2AnualKg = 100, ArvoresNecessariasAnual = 10 };

            _historicoService.Salvar(usuario.Id, 5, ModoTransporte.Onibus, 5, resultado);
            _historicoService.Salvar(usuario.Id, 8, ModoTransporte.Metro, 2, resultado);
            _historicoService.Salvar(usuario.Id, 30, ModoTransporte.CarroGasolina, 7, resultado);

            Assert.Equal(3, _historicoService.ListarPorUsuario(usuario.Id).Count);
        }

        [Fact]
        public void ListarPorUsuario_DeveOrdenarDoMaisRecentePrimeiro()
        {
            var usuario = _usuarioService.Cadastrar("Teste", "ordem@teste.com", "senha123");
            var resultado = new ResultadoImpacto { Co2DiarioKg = 1, Co2AnualKg = 100, ArvoresNecessariasAnual = 10 };

            _historicoService.Salvar(usuario.Id, 10, ModoTransporte.Moto, 3, resultado);
            Thread.Sleep(1100); // a data e gravada com precisao de segundos
            _historicoService.Salvar(usuario.Id, 20, ModoTransporte.Moto, 3, resultado);

            var lista = _historicoService.ListarPorUsuario(usuario.Id);

            Assert.Equal(20, lista[0].DistanciaDiariaKm);
            Assert.Equal(10, lista[1].DistanciaDiariaKm);
        }

        [Theory]
        [InlineData(ModoTransporte.BicicletaCaminhada)]
        [InlineData(ModoTransporte.Metro)]
        [InlineData(ModoTransporte.Onibus)]
        [InlineData(ModoTransporte.Moto)]
        [InlineData(ModoTransporte.CarroGasolina)]
        public void Salvar_CadaMeioDeTransporte_DeveSerRecuperadoCorretamente(ModoTransporte modo)
        {
            // O meio e gravado como texto no banco e convertido de volta para o enum na leitura
            var usuario = _usuarioService.Cadastrar("Teste", "modo@teste.com", "senha123");
            var resultado = new ResultadoImpacto { Co2DiarioKg = 1, Co2AnualKg = 100, ArvoresNecessariasAnual = 10 };

            _historicoService.Salvar(usuario.Id, 10, modo, 3, resultado);

            var lista = _historicoService.ListarPorUsuario(usuario.Id);
            Assert.Equal(modo, lista[0].Modo);
        }

        [Fact]
        public void ListarPorUsuario_UsuarioInexistente_DeveRetornarListaVazia()
        {
            Assert.Empty(_historicoService.ListarPorUsuario(9999));
        }
    }
}