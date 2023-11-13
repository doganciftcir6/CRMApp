using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onicorn.CRMApp.DataAccess.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(x => x.SalesAmount).IsRequired().HasMaxLength(50);
            builder.Property(x => x.SalesDate).IsRequired().HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.Project).WithMany(x => x.Sales).HasForeignKey(x => x.ProjectId);
            builder.HasOne(x => x.Customer).WithMany(x => x.Sales).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.SaleSituation).WithMany(x => x.Sales).HasForeignKey(x => x.SaleSituationId);
        }
    }
}
