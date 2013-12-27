using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class JobMap : EntityTypeConfiguration<Job>
    {
        public JobMap()
        {
            // Primary Key
            this.HasKey(t => t.JobID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Job");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.ResultInfo).HasColumnName("ResultInfo");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.JobGroupID).HasColumnName("JobGroupID");

            // Relationships
            this.HasRequired(t => t.JobGroup)
                .WithMany(t => t.Jobs)
                .HasForeignKey(d => d.JobGroupID);
            this.HasRequired(t => t.Task)
                .WithMany(t => t.Jobs)
                .HasForeignKey(d => d.TaskID);

        }
    }
}
