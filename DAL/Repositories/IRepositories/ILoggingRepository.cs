using DAL.GenericRepositories.IGenericRepositories;
using Entity.Entities;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface ILoggingRepository : IGenericRepository<RequestLog>
    {
        Task AddLog(RequestLog requestLog);
    }
}
