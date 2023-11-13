using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Onicorn.CRMApp.Entities.Task;

namespace Onicorn.CRMApp.DataAccess.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.Property(x => x.Taskname).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.StartDate).IsRequired().HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.TaskSituation).WithMany(x => x.Tasks).HasForeignKey(x => x.TaskSituationId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Tasks).HasForeignKey(x => x.AppUserId);
        }
    }
}
