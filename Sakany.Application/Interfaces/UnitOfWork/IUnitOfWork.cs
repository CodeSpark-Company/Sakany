using Sakany.Application.Repositories.Base;

namespace Sakany.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IBaseRepository<T> Repository<T>() where T : class;

        Task<int> SaveAsync();
    }
}