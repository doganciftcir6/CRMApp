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
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(new Gender[]
            {
                new()
                {
                    Definition = "Male",
                    Id = 1,
                },
                new()
                {
                    Definition = "Female",
                    Id = 2,
                },
                 new()
                {
                    Definition = "I don't want to specify",
                    Id = 3
                }
            });
            builder.Property(x => x.Definition).IsRequired().HasMaxLength(100);
        }
    }
}
