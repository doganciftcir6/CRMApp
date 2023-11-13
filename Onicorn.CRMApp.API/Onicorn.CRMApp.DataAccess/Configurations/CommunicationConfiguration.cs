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
    public class CommunicationConfiguration : IEntityTypeConfiguration<Communication>
    {
        public void Configure(EntityTypeBuilder<Communication> builder)
        {
            builder.Property(x => x.CommunicationDate).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(x => x.Detail).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.InsertTime).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.Customer).WithMany(x => x.Communications).HasForeignKey(x => x.CustomerId);
            builder.HasOne(x => x.CommunicationType).WithMany(x => x.Communications).HasForeignKey(x => x.CommunicationTypeId);
        }
    }
}
