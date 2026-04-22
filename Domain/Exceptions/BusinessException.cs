namespace good_hamburguer_system.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}