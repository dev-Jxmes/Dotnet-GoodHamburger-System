namespace good_hamburguer_system.Domain.Exceptions
{
    public class ValidationException : AppException
    {
        public ValidationException(string message)
            : base(message, 422)
        {
        }
    }
}