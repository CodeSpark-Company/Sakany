using Sakany.Application.Interfaces.UnitOfWork;
using Sakany.Application.Repositories.Base;
using Sakany.Persistence.DbContexts;
using Sakany.Persistence.Repositories.Base;
using System.Collections.Concurrent;

namespace Sakany.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties

        private readonly SakanyDbContext _context;

        private ConcurrentDictionary<string, object> _repositories;

        #endregion Properties

        #region Constructors

        public UnitOfWork(SakanyDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<string, object>();
        }

        #endregion Constructors

        #region Methods

        public IBaseRepository<T> Repository<T>() where T : class
        {
            string type = typeof(T).Name;

            BaseRepository<T> repository = new(_context);

            return (IBaseRepository<T>)_repositories.GetOrAdd(type, repository);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        #endregion Methods
    }
}