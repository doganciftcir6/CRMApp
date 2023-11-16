using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.DataAccess.Contexts.EntityFramework;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.DataAccess.Repositories.Concrete
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public override async Task<IEnumerable<Project>> GetAllFilterAsync(Expression<Func<Project, bool>> filter)
        {
            return await _appDbContext.Set<Project>().Where(filter).OrderByDescending(x => x.InsertTime).ToListAsync();
        }
    }
}
