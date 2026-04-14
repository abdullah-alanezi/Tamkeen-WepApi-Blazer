using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities.Interview;

namespace Tamkeen.Persistence.Configurations
{
    public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.InterviewerName)
                .HasMaxLength(150);

            builder.Property(x => x.MeetingLink)
                .HasMaxLength(500);

            builder.Property(x => x.Location)
                .HasMaxLength(200);

            builder.Property(x => x.Feedback)
                .HasMaxLength(2000);

            // إعداد العلاقة: الطلب الواحد يمكن أن يمر بعدة مقابلات (مثلاً مقابلة تقنية ثم HR)
            builder.HasOne(x => x.TrainingApplication)
                .WithMany(x => x.Interviews)
                .HasForeignKey(x => x.TrainingApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
