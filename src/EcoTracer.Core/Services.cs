using EcoTracer.Core.Models;

namespace EcoTracer.Core.Services
{
    public class CalculadoraEcoTracer
    {
        public ResultadoImpacto Calcular(double distanciaDiariaKm, ModoTransporte modo)
        {
            double fatorEmissao = modo switch
            {
                ModoTransporte.BicicletaCaminhada => 0.0,
                ModoTransporte.Metro => 0.030,
                ModoTransporte.Onibus => 0.080,
                ModoTransporte.Moto => 0.110,
                ModoTransporte.CarroGasolina => 0.190,
                _ => 0.190
            };

            double co2Diario = distanciaDiariaKm * fatorEmissao;
            double co2Anual = co2Diario * 260; // 260 dias úteis/ano
            int arvores = (int)Math.Ceiling(co2Anual / 21.7);

            return new ResultadoImpacto
            {
                Co2DiarioKg = Math.Round(co2Diario, 3),
                Co2AnualKg = Math.Round(co2Anual, 3),
                ArvoresNecessariasAnual = arvores
            };
        }
    }
}
