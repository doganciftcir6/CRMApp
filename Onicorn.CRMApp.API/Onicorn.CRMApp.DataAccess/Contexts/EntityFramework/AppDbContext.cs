using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Onicorn.CRMApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Onicorn.CRMApp.Entities.Task;

namespace Onicorn.CRMApp.DataAccess.Contexts.EntityFramework
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Communication> Communications { get; set; }
        public DbSet<CommunicationType> CommunicationTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleSituation> SaleSituations { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskSituation> TaskSituations { get; set; }
    }
}
