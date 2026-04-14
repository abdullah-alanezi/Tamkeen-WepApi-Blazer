using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Persistence.Configurations
{
    public class TrainingApplicationConfiguration : IEntityTypeConfiguration<TrainingApplication>
    {
        public void Configure(EntityTypeBuilder<TrainingApplication> builder)
        {
            builder.HasKey(x => x.Id);

            // منع حذف المتدرب إذا كان لديه طلب تقديم نشط
            builder.HasOne(x => x.Trainee)
                .WithMany(x => x.AppliedPrograms)
                .HasForeignKey(x => x.TraineeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.HRFeedback).HasMaxLength(1000);
        }
    }
}
