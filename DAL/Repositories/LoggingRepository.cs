using DAL.DatabaseContext;
using DAL.GenericRepositories;
using DAL.Repositories.IRepositories;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LoggingRepository : GenericRepository<RequestLog>, ILoggingRepository
    {
        private readonly DataContext _dataContext;
        public LoggingRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddLog(RequestLog requestLog)
        {
            await _dataContext.RequestLogs.AddAsync(requestLog);
            await _dataContext.SaveChangesAsync();
        }
    }
}
