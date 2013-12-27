using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class Task_TaskGroupMap : EntityTypeConfiguration<Task_TaskGroup>
    {
        public Task_TaskGroupMap()
        {
            // Primary Key
            this.HasKey(t => new { t.TaskID, t.TaskGroupID });

            // Properties
            this.Property(t => t.TaskID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TaskGroupID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Task_TaskGroup");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.TaskGroupID).HasColumnName("TaskGroupID");
            this.Property(t => t.TaskOrder).HasColumnName("TaskOrder");

            // Relationships
            this.HasRequired(t => t.Task)
                .WithMany(t => t.Task_TaskGroup)
                .HasForeignKey(d => d.TaskID);
            this.HasRequired(t => t.TaskGroup)
                .WithMany(t => t.Task_TaskGroup)
                .HasForeignKey(d => d.TaskGroupID);

        }
    }
}
