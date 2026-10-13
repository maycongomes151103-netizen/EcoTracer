namespace EcoTracer.Core.Models
{
    public enum ModoTransporte
    {
        BicicletaCaminhada,
        Metro,
        Onibus,
        Moto,
        CarroGasolina
    }

    public class ResultadoImpacto
    {
        public double Co2DiarioKg { get; set; }
        public double Co2AnualKg { get; set; }
        public int ArvoresNecessariasAnual { get; set; }
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
    }
        public class HistoricoCalculo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public double DistanciaDiariaKm { get; set; }
        public ModoTransporte Modo { get; set; }
        public int DiasPorSemana { get; set; }
        public double Co2AnualKg { get; set; }
        public int ArvoresNecessariasAnual { get; set; }
        public DateTime DataCalculo { get; set; }
    }
}