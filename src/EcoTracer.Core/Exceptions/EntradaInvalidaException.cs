using System;

namespace EcoTracer.Core.Exceptions
{
    public class EntradaInvalidaException : Exception
    {
        public EntradaInvalidaException(string message) : base(message) { }
    }
}