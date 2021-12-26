using DAL.DatabaseContext;
using DAL.GenericRepositories;
using DAL.Repositories.IRepositories;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<string> GetUserSalt(string userName)
        {
            User user = await _dataContext.Users.SingleOrDefaultAsync(m => m.Username == userName);
            if(user == null)
            {
                return null;
            }
            return user.Salt;
        }

        public async Task<bool> IsUserExist(string userName, int? userId)
        {
            return  await _dataContext.Users.AnyAsync(m => m.Username == userName && m.UserId != userId);
        }
    }
}
