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
            this.Property(t => t.JobName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ExecutePath)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Job");
            this.Property(t => t.JobID).HasColumnName("JobID");
            this.Property(t => t.JobName).HasColumnName("JobName");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.ExecutePath).HasColumnName("ExecutePath");
            this.Property(t => t.ExecuteArgs).HasColumnName("ExecuteArgs");
            this.Property(t => t.JobGroupID).HasColumnName("JobGroupID");
            this.Property(t => t.JobOrder).HasColumnName("JobOrder");
            this.Property(t => t.JobResult).HasColumnName("JobResult");
            this.Property(t => t.JobDetailResult).HasColumnName("JobDetailResult");

            // Relationships
            this.HasRequired(t => t.JobGroup)
                .WithMany(t => t.Jobs)
                .HasForeignKey(d => d.JobGroupID);

        }
    }
}
