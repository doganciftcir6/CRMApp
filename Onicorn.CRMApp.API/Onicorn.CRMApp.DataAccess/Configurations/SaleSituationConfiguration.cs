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
    public class SaleSituationConfiguration : IEntityTypeConfiguration<SaleSituation>
    {
        public void Configure(EntityTypeBuilder<SaleSituation> builder)
        {
            builder.HasData(new SaleSituation[]
           {
                new()
                {
                    Id = 1,
                    Definition = "Completed"
                },
                new()
                {
                    Id = 2,
                    Definition = "Continues"
                },
                 new()
                {
                    Id = 3,
                    Definition = "Cancelled"
                }
           });
            builder.Property(x => x.Definition).IsRequired().HasMaxLength(100);
        }
    }
}
