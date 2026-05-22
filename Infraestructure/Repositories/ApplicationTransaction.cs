using Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructure.Repositories
{
    public class ApplicationTransaction : IApplicationTransaction
    {
        private readonly IDbContextTransaction _efCoreTransaction;
        public ApplicationTransaction(IDbContextTransaction efCoreTransaction)
        {
            _efCoreTransaction = efCoreTransaction;
        }
        public Task CommitAsync()
        {
            return _efCoreTransaction.CommitAsync();
        }

        public Task RollBackAsync()
        {
            return _efCoreTransaction.RollbackAsync();
        }

        public void Dispose()
        {
            _efCoreTransaction.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await _efCoreTransaction.DisposeAsync();
        }
    }
    
}
