namespace Domain.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
    public NotFoundException(string name, object key, string property) : base($"Entity \"{name}\" ({key}) with {property} was not found.")
    {
    }
    public NotFoundException(string name, string property, object value) : base($"Entity \"{name}\" with {property} ({value}) was not found.")
    {
    }
}