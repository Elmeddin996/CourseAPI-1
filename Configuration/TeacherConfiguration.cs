using CourseApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Configuration
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(25).IsRequired(true);
            builder.HasOne(x => x.Group).WithMany(x => x.Teachers).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
