using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class JobAssignmentMap : EntityTypeConfiguration<JobAssignment>
    {
        public JobAssignmentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ClientID, t.JobGroupID });

            // Properties
            this.Property(t => t.ClientID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.JobGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("JobAssignment");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.JobGroupID).HasColumnName("JobGroupID");
            this.Property(t => t.JobAssignmentDateTime).HasColumnName("JobAssignmentDateTime");

            // Relationships
            this.HasRequired(t => t.JobGroup)
                .WithMany(t => t.JobAssignments)
                .HasForeignKey(d => d.JobGroupID);

        }
    }
}
