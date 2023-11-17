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
            return await _appDbContext.Set<Sale>().Where(filter).OrderByDescending(x => x.SalesDate).ToListAsync();
        }
    }
}
