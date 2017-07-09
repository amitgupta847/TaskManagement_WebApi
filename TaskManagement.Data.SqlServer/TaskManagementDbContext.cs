using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TaskManagement.Data.SqlServer.DataEntities;

namespace TaskManagement.Data.SqlServer
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext() : base("TaskManagementDbContextConnString")
        {
            this.Database.Log = msg => Debug.WriteLine(msg);
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            modelBuilder.Entity<Task>()
                .HasMany<User>(t => t.Users)
                .WithMany(u => u.Tasks)
                .Map(cs =>
               {
                   cs.MapLeftKey("TaskId");
                   cs.MapRightKey("UserId");
                   cs.ToTable("TaskUser");
               }
                );
        }
    }
}

