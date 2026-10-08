using EcoTracer.Core.Exceptions;
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
            var resultado = _calculadora.Calcular(20, ModoTransporte.BicicletaCaminhada, 5);
            Assert.Equal(0, resultado.Co2DiarioKg);
            Assert.Equal(0, resultado.Co2AnualKg);
        }

        [Fact]
        public void Calcular_CarroGasolina_DeveCalcularEmissaoDiariaCorretamente()
        {
            var resultado = _calculadora.Calcular(20, ModoTransporte.CarroGasolina, 5);
            Assert.Equal(3.5, resultado.Co2DiarioKg, 3);
        }

        [Fact]
        public void Calcular_CarroGasolina_DeveCalcularEmissaoAnualCorretamente()
        {
            var resultado = _calculadora.Calcular(20, ModoTransporte.CarroGasolina, 5);
            Assert.Equal(910.0, resultado.Co2AnualKg, 2);
        }

        [Fact]
        public void Calcular_CarroGasolina_DeveCalcularArvoresCorretamente()
        {
            var resultado = _calculadora.Calcular(20, ModoTransporte.CarroGasolina, 5);
            Assert.Equal(112, resultado.ArvoresNecessariasAnual);
        }

        [Fact]
        public void Calcular_DistanciaZero_DeveLancarExcecao()
        {
            Assert.Throws<DistanciaInvalidaException>(
                () => _calculadora.Calcular(0, ModoTransporte.CarroGasolina, 5));
        }

        [Fact]
        public void Calcular_DistanciaNegativa_DeveLancarExcecao()
        {
            Assert.Throws<DistanciaInvalidaException>(
                () => _calculadora.Calcular(-5, ModoTransporte.CarroGasolina, 5));
        }

        [Fact]
        public void Calcular_DiasForaDoIntervalo_DeveLancarExcecao()
        {
            Assert.Throws<DiasInvalidosException>(
                () => _calculadora.Calcular(20, ModoTransporte.CarroGasolina, 8));
        }

        [Fact]
        public void Calcular_Metro_DeveUsarFatorCorreto()
        {
            var resultado = _calculadora.Calcular(10, ModoTransporte.Metro, 5);
            Assert.Equal(0.04, resultado.Co2DiarioKg, 3);
        }
    }
}