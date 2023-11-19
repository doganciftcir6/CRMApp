using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.DataAccess.Contexts.EntityFramework;
using Onicorn.CRMApp.DataAccess.Repositories.Interfaces;
using Onicorn.CRMApp.Entities;
using System.Linq.Expressions;

namespace Onicorn.CRMApp.DataAccess.Repositories.Concrete
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        public SaleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public override async Task<IEnumerable<Sale>> GetAllFilterAsync(Expression<Func<Sale, bool>> filter)
        {
            return await _appDbContext.Set<Sale>().Include(x => x.Customer).Include(x => x.Project).Include(x => x.SaleSituation).Where(filter).OrderByDescending(x => x.SalesDate).ToListAsync();
        }

        public override async Task<Sale> GetByFilterAsync(Expression<Func<Sale, bool>> filter)
        {
            return await _appDbContext.Set<Sale>().Include(x => x.Customer).Include(x => x.Project).Include(x => x.SaleSituation).Where(filter).OrderByDescending(x => x.SalesDate).SingleOrDefaultAsync();
        }
    }
}
