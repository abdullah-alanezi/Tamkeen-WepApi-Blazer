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

            builder.Property(x => x.HRFeedback)
                .HasMaxLength(1000);

            builder.HasOne(x => x.Trainee)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => x.TraineeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.TrainingProgram)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => x.TrainingProgramId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
