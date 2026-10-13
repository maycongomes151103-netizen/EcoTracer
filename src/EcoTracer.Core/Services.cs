using EcoTracer.Core.Exceptions;
using EcoTracer.Core.Models;
using EcoTracer.Core.Data;
using BCrypt.Net;

namespace EcoTracer.Core.Services
{
    public class CalculadoraEcoTracer
    {
        // Fatores de emissao (kg CO2/km). Fontes em docs/fatores-emissao.md.
        private static readonly Dictionary<ModoTransporte, double> FatoresEmissaoKgKm = new()
        {
            { ModoTransporte.CarroGasolina, 0.175 },
            { ModoTransporte.Moto, 0.052 },
            { ModoTransporte.Onibus, 0.038 },
            { ModoTransporte.Metro, 0.004 },
            { ModoTransporte.BicicletaCaminhada, 0.0 },
        };

        private const double AbsorcaoArvoreKgAno = 8.16;
        private const int SemanasPorAno = 52;

        private void Validar(double distanciaDiariaKm, int diasPorSemana)
        {
            if (distanciaDiariaKm <= 0)
            {
                throw new DistanciaInvalidaException(
                    $"Distancia deve ser maior que zero, recebido: {distanciaDiariaKm}");
            }

            if (diasPorSemana < 1 || diasPorSemana > 7)
            {
                throw new DiasInvalidosException(
                    $"Dias por semana deve estar entre 1 e 7, recebido: {diasPorSemana}");
            }
        }

        public ResultadoImpacto Calcular(double distanciaDiariaKm, ModoTransporte modo, int diasPorSemana)
        {
            Validar(distanciaDiariaKm, diasPorSemana);

            double fator = FatoresEmissaoKgKm[modo];
            double co2Diario = distanciaDiariaKm * fator;
            double co2Anual = co2Diario * diasPorSemana * SemanasPorAno;
            int arvores = (int)Math.Ceiling(co2Anual / AbsorcaoArvoreKgAno);

            return new ResultadoImpacto
            {
                Co2DiarioKg = Math.Round(co2Diario, 3),
                Co2AnualKg = Math.Round(co2Anual, 2),
                ArvoresNecessariasAnual = arvores
            };
        }
    }
        public class UsuarioService
    {
        private readonly AppDatabase _db;

        public UsuarioService(AppDatabase db)
        {
            _db = db;
        }

        public Usuario Cadastrar(string nome, string email, string senha)
        {
            using var conexao = _db.CriarConexao();
            conexao.Open();

            var verifica = conexao.CreateCommand();
            verifica.CommandText = "SELECT COUNT(*) FROM Usuarios WHERE Email = $email";
            verifica.Parameters.AddWithValue("$email", email);
            var existe = (long)verifica.ExecuteScalar()! > 0;

            if (existe)
                throw new EmailJaCadastradoException($"Já existe uma conta com o email: {email}");

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

            var insercao = conexao.CreateCommand();
            insercao.CommandText = @"
                INSERT INTO Usuarios (Nome, Email, SenhaHash)
                VALUES ($nome, $email, $senhaHash);
                SELECT last_insert_rowid();";
            insercao.Parameters.AddWithValue("$nome", nome);
            insercao.Parameters.AddWithValue("$email", email);
            insercao.Parameters.AddWithValue("$senhaHash", senhaHash);

            var novoId = (long)insercao.ExecuteScalar()!;

            return new Usuario { Id = (int)novoId, Nome = nome, Email = email, SenhaHash = senhaHash };
        }

        public Usuario Autenticar(string email, string senha)
        {
            using var conexao = _db.CriarConexao();
            conexao.Open();

            var busca = conexao.CreateCommand();
            busca.CommandText = "SELECT Id, Nome, Email, SenhaHash FROM Usuarios WHERE Email = $email";
            busca.Parameters.AddWithValue("$email", email);

            using var leitor = busca.ExecuteReader();
            if (!leitor.Read())
                throw new CredenciaisInvalidasException("Email ou senha incorretos.");

            var usuario = new Usuario
            {
                Id = leitor.GetInt32(0),
                Nome = leitor.GetString(1),
                Email = leitor.GetString(2),
                SenhaHash = leitor.GetString(3)
            };

            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
                throw new CredenciaisInvalidasException("Email ou senha incorretos.");

            return usuario;
        }
    }
        public class HistoricoService
    {
        private readonly AppDatabase _db;

        public HistoricoService(AppDatabase db)
        {
            _db = db;
        }

        public void Salvar(int usuarioId, double distanciaDiariaKm, ModoTransporte modo,
                            int diasPorSemana, ResultadoImpacto resultado)
        {
            using var conexao = _db.CriarConexao();
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                INSERT INTO Historico
                    (UsuarioId, DistanciaDiariaKm, Modo, DiasPorSemana, Co2AnualKg, ArvoresNecessariasAnual, DataCalculo)
                VALUES
                    ($usuarioId, $distancia, $modo, $dias, $co2Anual, $arvores, $data);";
            comando.Parameters.AddWithValue("$usuarioId", usuarioId);
            comando.Parameters.AddWithValue("$distancia", distanciaDiariaKm);
            comando.Parameters.AddWithValue("$modo", modo.ToString());
            comando.Parameters.AddWithValue("$dias", diasPorSemana);
            comando.Parameters.AddWithValue("$co2Anual", resultado.Co2AnualKg);
            comando.Parameters.AddWithValue("$arvores", resultado.ArvoresNecessariasAnual);
            comando.Parameters.AddWithValue("$data", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            comando.ExecuteNonQuery();
        }

        public List<HistoricoCalculo> ListarPorUsuario(int usuarioId)
        {
            var lista = new List<HistoricoCalculo>();

            using var conexao = _db.CriarConexao();
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = @"
                SELECT Id, UsuarioId, DistanciaDiariaKm, Modo, DiasPorSemana, Co2AnualKg, ArvoresNecessariasAnual, DataCalculo
                FROM Historico
                WHERE UsuarioId = $usuarioId
                ORDER BY DataCalculo DESC;";
            comando.Parameters.AddWithValue("$usuarioId", usuarioId);

            using var leitor = comando.ExecuteReader();
            while (leitor.Read())
            {
                lista.Add(new HistoricoCalculo
                {
                    Id = leitor.GetInt32(0),
                    UsuarioId = leitor.GetInt32(1),
                    DistanciaDiariaKm = leitor.GetDouble(2),
                    Modo = Enum.Parse<ModoTransporte>(leitor.GetString(3)),
                    DiasPorSemana = leitor.GetInt32(4),
                    Co2AnualKg = leitor.GetDouble(5),
                    ArvoresNecessariasAnual = leitor.GetInt32(6),
                    DataCalculo = DateTime.Parse(leitor.GetString(7))
                });
            }

            return lista;
        }
    }
}