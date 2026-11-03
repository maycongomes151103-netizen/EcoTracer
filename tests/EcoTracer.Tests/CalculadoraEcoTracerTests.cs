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

        // ---------- Testes originais ----------

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

        // ---------- Fatores dos meios de transporte que faltavam ----------

        [Fact]
        public void Calcular_Moto_DeveUsarFatorCorreto()
        {
            // 10 km x 0,052 kg/km = 0,52 kg por dia
            var resultado = _calculadora.Calcular(10, ModoTransporte.Moto, 5);
            Assert.Equal(0.52, resultado.Co2DiarioKg, 3);
        }

        [Fact]
        public void Calcular_Onibus_DeveUsarFatorCorreto()
        {
            // 10 km x 0,038 kg/km = 0,38 kg por dia
            var resultado = _calculadora.Calcular(10, ModoTransporte.Onibus, 5);
            Assert.Equal(0.38, resultado.Co2DiarioKg, 3);
        }

        // ---------- Arredondamentos ----------

        [Fact]
        public void Calcular_ArvoresNaoExatas_DeveArredondarParaCima()
        {
            // Moto, 10 km, 1 dia: 0,52 x 1 x 52 = 27,04 kg/ano. 27,04 / 8,16 = 3,31 -> 4 arvores
            var resultado = _calculadora.Calcular(10, ModoTransporte.Moto, 1);
            Assert.Equal(4, resultado.ArvoresNecessariasAnual);
        }

        [Fact]
        public void Calcular_Bicicleta_DeveRetornarZeroArvores()
        {
            var resultado = _calculadora.Calcular(20, ModoTransporte.BicicletaCaminhada, 5);
            Assert.Equal(0, resultado.ArvoresNecessariasAnual);
        }

        [Fact]
        public void Calcular_EmissaoDiaria_DeveArredondarParaTresCasas()
        {
            // 1,23456 x 0,175 = 0,216048 -> 0,216
            var resultado = _calculadora.Calcular(1.23456, ModoTransporte.CarroGasolina, 3);
            Assert.Equal(0.216, resultado.Co2DiarioKg, 5);
        }

        [Fact]
        public void Calcular_EmissaoAnual_DeveArredondarParaDuasCasas()
        {
            // 0,216048 x 3 x 52 = 33,703488 -> 33,70
            var resultado = _calculadora.Calcular(1.23456, ModoTransporte.CarroGasolina, 3);
            Assert.Equal(33.7, resultado.Co2AnualKg, 4);
        }

        // ---------- Limites do intervalo de dias ----------

        [Theory]
        [InlineData(1, 182.0)]    // 3,5 x 1 x 52
        [InlineData(7, 1274.0)]   // 3,5 x 7 x 52
        public void Calcular_DiasNosLimitesDoIntervalo_NaoDeveLancarExcecao(int dias, double anualEsperado)
        {
            var resultado = _calculadora.Calcular(20, ModoTransporte.CarroGasolina, dias);
            Assert.Equal(anualEsperado, resultado.Co2AnualKg, 2);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Calcular_DiasAbaixoDoMinimo_DeveLancarExcecao(int dias)
        {
            Assert.Throws<DiasInvalidosException>(
                () => _calculadora.Calcular(20, ModoTransporte.CarroGasolina, dias));
        }

        // ---------- Entradas invalidas ----------

        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.PositiveInfinity)]
        [InlineData(double.NegativeInfinity)]
        public void Calcular_DistanciaNaoNumerica_DeveLancarExcecao(double distancia)
        {
            Assert.Throws<DistanciaInvalidaException>(
                () => _calculadora.Calcular(distancia, ModoTransporte.CarroGasolina, 5));
        }

        [Fact]
        public void Calcular_ModoTransporteInexistente_DeveLancarExcecao()
        {
            var modoInvalido = (ModoTransporte)99;

            Assert.Throws<EntradaInvalidaException>(
                () => _calculadora.Calcular(20, modoInvalido, 5));
        }

        [Fact]
        public void Calcular_DistanciaNegativa_MensagemDeveInformarOValorRecebido()
        {
            var excecao = Assert.Throws<DistanciaInvalidaException>(
                () => _calculadora.Calcular(-5, ModoTransporte.CarroGasolina, 5));

            Assert.Contains("-5", excecao.Message);
        }

        [Fact]
        public void Calcular_ErrosDeEntrada_PodemSerCapturadosPelaClasseBase()
        {
            // E o que permite a tela usar um unico catch (EntradaInvalidaException)
            Assert.ThrowsAny<EntradaInvalidaException>(
                () => _calculadora.Calcular(-1, ModoTransporte.Moto, 3));
            Assert.ThrowsAny<EntradaInvalidaException>(
                () => _calculadora.Calcular(10, ModoTransporte.Moto, 9));
        }
    }
}