
namespace Domain.Exceptions;

public class ItemAmountExceededException : Exception
{
    public ItemAmountExceededException(string message):base(message){}
    public ItemAmountExceededException(string message, Exception innerException):base(message, innerException){}
}