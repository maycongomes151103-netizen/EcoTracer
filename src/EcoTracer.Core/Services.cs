using EcoTracer.Core.Exceptions;
using EcoTracer.Core.Models;

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
}