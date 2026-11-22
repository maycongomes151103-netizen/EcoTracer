namespace EcoTracer.Core.Exceptions
{
        public class CredenciaisInvalidasException : EntradaInvalidaException
    {
        public CredenciaisInvalidasException(string mensagem) : base(mensagem) { }
    }

    public class EmailJaCadastradoException : EntradaInvalidaException
    {
        public EmailJaCadastradoException(string mensagem) : base(mensagem) { }
    }
}