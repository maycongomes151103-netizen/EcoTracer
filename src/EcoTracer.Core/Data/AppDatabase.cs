using Microsoft.Data.Sqlite;

namespace EcoTracer.Core.Data
{
    public class AppDatabase
    {
        private readonly string _connectionString;

        public AppDatabase(string caminhoArquivoDb = "ecotracer.db")
        {
            _connectionString = $"Data Source={caminhoArquivoDb}";
        }

        public SqliteConnection CriarConexao() => new SqliteConnection(_connectionString);

        public void Inicializar()
        {
            using var conexao = CriarConexao();
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                CREATE TABLE IF NOT EXISTS Usuarios (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nome TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    SenhaHash TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Historico (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    UsuarioId INTEGER NOT NULL,
                    DistanciaDiariaKm REAL NOT NULL,
                    Modo TEXT NOT NULL,
                    DiasPorSemana INTEGER NOT NULL,
                    Co2AnualKg REAL NOT NULL,
                    ArvoresNecessariasAnual INTEGER NOT NULL,
                    DataCalculo TEXT NOT NULL,
                    FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id)
                );
            ";
            comando.ExecuteNonQuery();
        }
    }
}