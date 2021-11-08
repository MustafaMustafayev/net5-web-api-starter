using DAL.GenericRepositories.IGenericRepositories;
using Entity.Entities;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsUserExist(string userName, int? userId);
        Task<string> GetUserSalt(string userName);
    }
}
