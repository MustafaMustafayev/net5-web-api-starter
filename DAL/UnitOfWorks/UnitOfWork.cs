using DAL.DatabaseContext;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWorks.IUnitOfWorks;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public IUserRepository UserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
        public IAuthRepository AuthRepository { get; set; }
        public ILoggingRepository LoggingRepository { get; set; }
        private bool isDisposed = false;

        public UnitOfWork(DataContext dataContext, IUserRepository userRepository, IRoleRepository roleRepository, IAuthRepository authRepository, ILoggingRepository loggingRepository)
        {
            _dataContext = dataContext;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            AuthRepository = authRepository;
            LoggingRepository = loggingRepository;
        }

        public async Task CommitAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            }
        }

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _dataContext.DisposeAsync();
            }
        }
    }
}
