using DAL.DatabaseContext;
using DAL.Repositories.IRepositories;
using DAL.UnitOfWorks.IUnitOfWorks;
using System.Threading.Tasks;

namespace DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public IUserRepository UserRepository { get; set; }
        public IRoleRepository RoleRepository { get; set; }
        public IAuthRepository AuthRepository { get; set; }

        public UnitOfWork(DataContext dataContext, IUserRepository userRepository, IRoleRepository roleRepository, IAuthRepository authRepository)
        {
            _dataContext = dataContext;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
            AuthRepository = authRepository;
        }

        public async Task Commit()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
