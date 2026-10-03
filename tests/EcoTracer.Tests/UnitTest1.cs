using EcoTracer.Core.Models;
using EcoTracer.Core.Services;
using Xunit;

namespace EcoTracer.Tests
{
    public class CalculadoraEcoTracerTests
    {
        private readonly CalculadoraEcoTracer _calculadora;

        public CalculadoraEcoTracerTests()
        {
            _calculadora = new CalculadoraEcoTracer();
        }

        [Fact]
        public void Calcular_Bicicleta_DeveRetornarEmissaoZero()
        {
            double distancia = 15.0;
            var resultado = _calculadora.Calcular(distancia, ModoTransporte.BicicletaCaminhada);

            Assert.Equal(0, resultado.Co2DiarioKg);
            Assert.Equal(0, resultado.Co2AnualKg);
            Assert.Equal(0, resultado.ArvoresNecessariasAnual);
        }

        [Fact]
        public void Calcular_CarroGasolina_DeveCalcularEmissoesEArvoresCorretamente()
        {
            double distancia = 20.0;
            var resultado = _calculadora.Calcular(distancia, ModoTransporte.CarroGasolina);

            Assert.Equal(3.8, resultado.Co2DiarioKg, 3);
            Assert.Equal(988, resultado.Co2AnualKg, 3);
            Assert.Equal(46, resultado.ArvoresNecessariasAnual);
        }
    }
}
