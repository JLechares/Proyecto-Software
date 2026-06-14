namespace Application.Interfaces
{
    public interface IAppTransaction : IAsyncDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}

