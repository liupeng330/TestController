using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class JobGroupMap : EntityTypeConfiguration<JobGroup>
    {
        public JobGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.JobGroupID);

            // Properties
            // Table & Column Mappings
            this.ToTable("JobGroup");
            this.Property(t => t.JobGroupID).HasColumnName("JobGroupID");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.TaskGroupID).HasColumnName("TaskGroupID");

            // Relationships
            this.HasRequired(t => t.TaskGroup)
                .WithMany(t => t.JobGroups)
                .HasForeignKey(d => d.TaskGroupID);

        }
    }
}
