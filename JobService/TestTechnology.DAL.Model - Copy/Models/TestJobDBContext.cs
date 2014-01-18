using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TestTechnology.DAL.Models.Mapping;

namespace TestTechnology.DAL.Models
{
    public partial class TestJobDBContext : DbContext
    {
        static TestJobDBContext()
        {
            Database.SetInitializer<TestJobDBContext>(null);
        }

        public TestJobDBContext()
            : base("Name=TestJobDBContext")
        {
        }

        public DbSet<Client_JobGroup> Client_JobGroup { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobGroup> JobGroups { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Task_TaskGroup> Task_TaskGroup { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Client_JobGroupMap());
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new JobGroupMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TaskMap());
            modelBuilder.Configurations.Add(new Task_TaskGroupMap());
            modelBuilder.Configurations.Add(new TaskGroupMap());
        }
    }
}
