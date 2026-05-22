using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IApplicationTransaction: IDisposable, IAsyncDisposable
    {
        Task CommitAsync();
        Task RollBackAsync();
    }
}
