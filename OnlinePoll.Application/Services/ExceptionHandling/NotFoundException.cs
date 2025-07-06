namespace OnlinePoll.Application.Services.ExceptionHandling;

public class NotFoundException : Exception
{
    private readonly string _message;
    
    public override string Message => _message;

    public NotFoundException(string message)
    {
        _message = message;
    }
}