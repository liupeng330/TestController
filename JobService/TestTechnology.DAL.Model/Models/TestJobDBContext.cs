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

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobAssignment> JobAssignments { get; set; }
        public DbSet<JobGroup> JobGroups { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new JobMap());
            modelBuilder.Configurations.Add(new JobAssignmentMap());
            modelBuilder.Configurations.Add(new JobGroupMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
