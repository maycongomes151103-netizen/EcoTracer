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
        public class CredenciaisInvalidasException : EntradaInvalidaException
    {
        public CredenciaisInvalidasException(string mensagem) : base(mensagem) { }
    }

    public class EmailJaCadastradoException : EntradaInvalidaException
    {
        public EmailJaCadastradoException(string mensagem) : base(mensagem) { }
    }
}