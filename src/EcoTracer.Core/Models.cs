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
}
