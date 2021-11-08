using DAL.DatabaseContext;
using DAL.GenericRepositories;
using DAL.Repositories.IRepositories;
using Entity.Entities;

namespace DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly DataContext _dataContext;
        public RoleRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
