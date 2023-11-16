using Microsoft.EntityFrameworkCore;
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
    public class CommunicationRepository : GenericRepository<Communication>, ICommunicationRepository
    {
        public CommunicationRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async override Task<IEnumerable<Communication>> GetAllAsync()
        {
            return await _appDbContext.Set<Communication>().Include(x => x.CommunicationType).Include(x => x.Customer).Where(x => x.Status == true).OrderByDescending(x => x.InsertTime).ToListAsync();
        }
    }
}
