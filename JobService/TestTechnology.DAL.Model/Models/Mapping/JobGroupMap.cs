using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class JobGroupMap : EntityTypeConfiguration<JobGroup>
    {
        public JobGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.JobGroupId);

            // Properties
            this.Property(t => t.JobGroupName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("JobGroup");
            this.Property(t => t.JobGroupId).HasColumnName("JobGroupId");
            this.Property(t => t.JobGroupName).HasColumnName("JobGroupName");
            this.Property(t => t.JobGroupResult).HasColumnName("JobGroupResult");
        }
    }
}
