using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class TaskGroupMap : EntityTypeConfiguration<TaskGroup>
    {
        public TaskGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.TaskGroupID);

            // Properties
            this.Property(t => t.TaskGroupName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TaskGroup");
            this.Property(t => t.TaskGroupID).HasColumnName("TaskGroupID");
            this.Property(t => t.TaskGroupName).HasColumnName("TaskGroupName");
        }
    }
}
