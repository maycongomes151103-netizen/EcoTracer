using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using EcoTracer.Core.Data;
using EcoTracer.Core.Services;
using Microsoft.Data.Sqlite;
using Xunit;

namespace EcoTracer.Tests
{
    public class AppDatabaseTests : IDisposable
    {
        private readonly string _caminhoDb;
        private readonly AppDatabase _db;

        public AppDatabaseTests()
        {
            _caminhoDb = Path.Combine(Path.GetTempPath(), $"teste_banco_{Guid.NewGuid()}.db");
            _db = new AppDatabase(_caminhoDb);
        }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            if (File.Exists(_caminhoDb))
                File.Delete(_caminhoDb);
        }

        private List<string> ListarTabelas()
        {
            using var conexao = _db.CriarConexao();
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'";

            var tabelas = new List<string>();
            using var leitor = comando.ExecuteReader();
            while (leitor.Read())
                tabelas.Add(leitor.GetString(0));

            return tabelas;
        }

        private void InserirUsuario(object nome, string email, string senhaHash)
        {
            using var conexao = _db.CriarConexao();
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = "INSERT INTO Usuarios (Nome, Email, SenhaHash) VALUES ($nome, $email, $hash)";
            comando.Parameters.AddWithValue("$nome", nome);
            comando.Parameters.AddWithValue("$email", email);
            comando.Parameters.AddWithValue("$hash", senhaHash);
            comando.ExecuteNonQuery();
        }

        [Fact]
        public void Inicializar_DeveCriarTabelasUsuariosEHistorico()
        {
            _db.Inicializar();

            var tabelas = ListarTabelas();

            Assert.Contains("Usuarios", tabelas);
            Assert.Contains("Historico", tabelas);
        }

        [Fact]
        public void Inicializar_ChamadoDuasVezes_NaoDeveLancarExcecao()
        {
            var excecao = Record.Exception(() =>
            {
                _db.Inicializar();
                _db.Inicializar();
            });

            Assert.Null(excecao);
        }

        [Fact]
        public void Inicializar_NaoDevePerderDadosJaGravados()
        {
            _db.Inicializar();
            var usuarioService = new UsuarioService(_db);
            usuarioService.Cadastrar("Ana", "ana@teste.com", "senha123");

            _db.Inicializar(); // o app chama isso toda vez que abre

            var usuario = usuarioService.Autenticar("ana@teste.com", "senha123");
            Assert.Equal("Ana", usuario.Nome);
        }

        [Fact]
        public void CriarConexao_DeveRetornarConexaoFechada()
        {
            using var conexao = _db.CriarConexao();

            Assert.Equal(ConnectionState.Closed, conexao.State);
        }

        [Fact]
        public void Banco_EmailDuplicado_DeveSerRejeitadoPelaRestricaoUnique()
        {
            _db.Inicializar();
            InserirUsuario("Um", "repetido@teste.com", "hash");

            Assert.Throws<SqliteException>(() => InserirUsuario("Dois", "repetido@teste.com", "hash"));
        }

        [Fact]
        public void Banco_NomeNulo_DeveSerRejeitadoPelaRestricaoNotNull()
        {
            _db.Inicializar();

            Assert.Throws<SqliteException>(() => InserirUsuario(DBNull.Value, "nulo@teste.com", "hash"));
        }
    }
}