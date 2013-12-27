using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class JobAssigmentMap : EntityTypeConfiguration<JobAssigment>
    {
        public JobAssigmentMap()
        {
            // Primary Key
            this.HasKey(t => t.AssignmentID);

            // Properties
            this.Property(t => t.Owner)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("JobAssigment");
            this.Property(t => t.AssignmentID).HasColumnName("AssignmentID");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Result).HasColumnName("Result");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Owner).HasColumnName("Owner");
        }
    }
}
