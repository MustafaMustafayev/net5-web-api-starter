using DAL.DatabaseContext;
using DAL.GenericRepositories;
using DAL.Repositories.IRepositories;
using Entity.Entities;

namespace DAL.Repositories
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        private readonly DataContext _dataContext;
        public AuthRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
