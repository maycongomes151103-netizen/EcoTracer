using System;
using EcoTracer.Core.Services;
using Xunit;

namespace EcoTracer.Tests
{
    public class ConversorEntradaTests
    {
        [Theory]
        [InlineData("20", 20.0)]
        [InlineData("20,5", 20.5)]   // regressao do bug: "20,5" estava virando 205 em pt-BR
        [InlineData("20.5", 20.5)]
        [InlineData("  7,25  ", 7.25)]
        public void LerDistancia_TextoValido_DeveConverterCorretamente(string texto, double esperado)
        {
            Assert.Equal(esperado, ConversorEntrada.LerDistancia(texto));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("abc")]
        [InlineData("20,5,3")]
        [InlineData("1.000,5")]
        public void LerDistancia_TextoInvalido_DeveLancarFormatException(string? texto)
        {
            Assert.Throws<FormatException>(() => ConversorEntrada.LerDistancia(texto));
        }

        [Theory]
        [InlineData("5", 5)]
        [InlineData("  7 ", 7)]
        public void LerDias_TextoValido_DeveConverterCorretamente(string texto, int esperado)
        {
            Assert.Equal(esperado, ConversorEntrada.LerDias(texto));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("abc")]
        [InlineData("5,5")]
        [InlineData("2.5")]
        public void LerDias_TextoInvalido_DeveLancarFormatException(string? texto)
        {
            Assert.Throws<FormatException>(() => ConversorEntrada.LerDias(texto));
        }
    }
}