using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.DataAccess.Contexts.EntityFramework;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using System.Linq.Expressions;
using Task = Onicorn.CRMApp.Entities.Task;

namespace Onicorn.CRMApp.DataAccess.Repositories.Concrete
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public override async Task<IEnumerable<Task>> GetAllFilterAsync(Expression<Func<Task, bool>> filter)
        {
            return await _appDbContext.Set<Task>().Include(x => x.AppUser).Include(x => x.TaskSituation).Where(filter).OrderByDescending(x => x.StartDate).ToListAsync();
        }

        public override async Task<Task> GetByFilterAsync(Expression<Func<Task, bool>> filter)
        {
            return await _appDbContext.Set<Task>().Include(x => x.AppUser).Include(x => x.TaskSituation).Where(filter).OrderByDescending(x => x.StartDate).SingleOrDefaultAsync();
        }
    }
}
