using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TestTechnology.DAL.Models.Mapping
{
    public class TaskMap : EntityTypeConfiguration<Task>
    {
        public TaskMap()
        {
            // Primary Key
            this.HasKey(t => t.TaskID);

            // Properties
            this.Property(t => t.TaskName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TaskExecuteFilePath)
                .IsRequired();

            this.Property(t => t.TaskArgs)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Task");
            this.Property(t => t.TaskID).HasColumnName("TaskID");
            this.Property(t => t.TaskName).HasColumnName("TaskName");
            this.Property(t => t.TaskExecuteFilePath).HasColumnName("TaskExecuteFilePath");
            this.Property(t => t.TaskArgs).HasColumnName("TaskArgs");
        }
    }
}
