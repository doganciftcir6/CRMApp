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
    public class CommunicationTypeConfiguration : IEntityTypeConfiguration<CommunicationType>
    {
        public void Configure(EntityTypeBuilder<CommunicationType> builder)
        {
            builder.HasData(new CommunicationType[]
            {
                new()
                {
                    Id = 1,
                    Definition = "Phone",
                },
                new()
                {
                    Id = 2,
                    Definition = "Sms"
                },
                new()
                {
                    Id = 3,
                    Definition = "Email"
                },
                new()
                {
                    Id = 4,
                    Definition = "Face to Face"
                }
            });
            builder.Property(x => x.Definition).IsRequired().HasMaxLength(100);
        }
    }
}
