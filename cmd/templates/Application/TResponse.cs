namespace Core.Application.Reponses;
public class TResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; } = default!;
    public Pagination Pagination { get; set; } = new Pagination();
    public List<Error> Errors { get; set; } = [];
}

public struct Error
{
    public string Code { get; set; }
    public string Message { get; set; }
}