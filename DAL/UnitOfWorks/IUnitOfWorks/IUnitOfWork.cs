using DAL.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWorks.IUnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IUserRepository UserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
        public IAuthRepository AuthRepository { get; set; }
        public ILoggingRepository LoggingRepository { get; set; }
        public Task CommitAsync();
    }
}
