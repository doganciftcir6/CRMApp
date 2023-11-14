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
    public class AppRoleRepository : GenericRepository<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
