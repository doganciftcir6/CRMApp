using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Onicorn.CRMApp.Entities.Task;

namespace Onicorn.CRMApp.DataAccess.Repositories.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
    }
}
