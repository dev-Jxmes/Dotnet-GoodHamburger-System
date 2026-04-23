namespace good_hamburguer_system.Domain.Exceptions
{
    public class BusinessException : AppException
    {
        public BusinessException(string message)
            : base(message, 400)
        {
        }
    }
}