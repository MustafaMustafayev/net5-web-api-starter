using DAL.Repositories.IRepositories;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWorks.IUnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
        public IAuthRepository AuthRepository { get; set; }
        public Task Commit();
    }
}
