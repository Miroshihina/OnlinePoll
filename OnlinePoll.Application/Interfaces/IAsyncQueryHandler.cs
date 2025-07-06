namespace OnlinePoll.Application.Interfaces;

public interface IAsyncQueryHandler<T, TV>
{
    public Task<TV> Handle(T query, CancellationToken cancellationToken);
}