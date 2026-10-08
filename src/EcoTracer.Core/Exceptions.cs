namespace EcoTracer.Core.Exceptions
{
    public class EntradaInvalidaException : Exception
    {
        public EntradaInvalidaException(string mensagem) : base(mensagem) { }
    }

    public class DistanciaInvalidaException : EntradaInvalidaException
    {
        public DistanciaInvalidaException(string mensagem) : base(mensagem) { }
    }

    public class DiasInvalidosException : EntradaInvalidaException
    {
        public DiasInvalidosException(string mensagem) : base(mensagem) { }
    }
}