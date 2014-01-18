using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class Client_JobGroupMap : EntityTypeConfiguration<Client_JobGroup>
    {
        public Client_JobGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.AssignmentID);

            // Properties
            this.Property(t => t.ClientID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Owner)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Client_JobGroup");
            this.Property(t => t.AssignmentID).HasColumnName("AssignmentID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.JobGroupID).HasColumnName("JobGroupID");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Result).HasColumnName("Result");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.Owner).HasColumnName("Owner");

            // Relationships
            this.HasRequired(t => t.JobGroup)
                .WithMany(t => t.Client_JobGroup)
                .HasForeignKey(d => d.JobGroupID);

        }
    }
}
