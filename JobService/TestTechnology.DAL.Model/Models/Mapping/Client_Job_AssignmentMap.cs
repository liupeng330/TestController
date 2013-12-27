using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class Client_Job_AssignmentMap : EntityTypeConfiguration<Client_Job_Assignment>
    {
        public Client_Job_AssignmentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AssignmentID, t.ClientID, t.JobGroupID });

            // Properties
            this.Property(t => t.AssignmentID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClientID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.JobGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Client_Job_Assignment");
            this.Property(t => t.AssignmentID).HasColumnName("AssignmentID");
            this.Property(t => t.ClientID).HasColumnName("ClientID");
            this.Property(t => t.JobGroupID).HasColumnName("JobGroupID");

            // Relationships
            this.HasRequired(t => t.JobAssigment)
                .WithMany(t => t.Client_Job_Assignment)
                .HasForeignKey(d => d.AssignmentID);
            this.HasRequired(t => t.JobGroup)
                .WithMany(t => t.Client_Job_Assignment)
                .HasForeignKey(d => d.JobGroupID);

        }
    }
}
