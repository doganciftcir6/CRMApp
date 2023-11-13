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
    public class TaskSituationConfiguration : IEntityTypeConfiguration<TaskSituation>
    {
        public void Configure(EntityTypeBuilder<TaskSituation> builder)
        {
            builder.HasData(new TaskSituation[]
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
                    Definition = "Postponed"
                },
                 new()
                {
                    Id = 4,
                    Definition = "Cancelled"
                }
          });
            builder.Property(x => x.Definition).IsRequired().HasMaxLength(100);
        }
    }
}
