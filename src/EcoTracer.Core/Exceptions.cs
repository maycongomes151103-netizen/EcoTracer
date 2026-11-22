namespace EcoTracer.Core.Exceptions
{ 
    public class EmailJaCadastradoException : EntradaInvalidaException
    {
        public EmailJaCadastradoException(string mensagem) : base(mensagem) { }
    }
}