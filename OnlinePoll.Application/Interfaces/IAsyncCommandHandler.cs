namespace OnlinePoll.Application.Interfaces;

public interface IAsyncCommandHandler<in T, TV>
{
    public Task<TV> Handle(T request, CancellationToken cancellationToken);
}