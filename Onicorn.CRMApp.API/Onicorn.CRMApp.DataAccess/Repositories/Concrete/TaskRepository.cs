using Onicorn.CRMApp.DataAccess.Contexts.EntityFramework;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Onicorn.CRMApp.Entities.Task;

namespace Onicorn.CRMApp.DataAccess.Repositories.Concrete
{
    public class TaskRepository : GenericRepository<Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
