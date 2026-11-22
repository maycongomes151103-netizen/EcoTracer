namespace EcoTracer.Core.Exceptions
{
    public class EmailInvalidoException : EntradaInvalidaException
    {
        public EmailInvalidoException(string message) : base(message) { }
    }
}