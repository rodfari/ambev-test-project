namespace Domain.Exceptions;

public class MinimumItemRequiredException : Exception
{
    public MinimumItemRequiredException(string message) : base(message)
    {
    }

    public MinimumItemRequiredException(string message, Exception innerException) : base(message, innerException)
    {
    }
}