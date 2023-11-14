using Onicorn.CRMApp.DataAccess.Contexts.EntityFramework;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.DataAccess.Repositories.Concrete
{
    public class TaskSituationRepository : GenericRepository<TaskSituation>, ITaskSituationRepository
    {
        public TaskSituationRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
