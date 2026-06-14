using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructure.Repositories
{
    public class EfAppTransaction : IAppTransaction
    {
        private readonly IDbContextTransaction _transaction;

        public EfAppTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _transaction.DisposeAsync();
        }
    }
}