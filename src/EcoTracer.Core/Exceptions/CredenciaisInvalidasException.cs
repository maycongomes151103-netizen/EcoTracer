namespace EcoTracer.Core.Exceptions
{
    public class CredenciaisInvalidasException : EntradaInvalidaException
    {
        public CredenciaisInvalidasException(string message) : base(message) { }
    }
}