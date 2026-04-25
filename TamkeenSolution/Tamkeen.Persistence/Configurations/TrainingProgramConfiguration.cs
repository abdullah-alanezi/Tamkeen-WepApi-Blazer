using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Persistence.Configurations
{
    public class TrainingProgramConfiguration : IEntityTypeConfiguration<TrainingProgram>
    {
        public void Configure(EntityTypeBuilder<TrainingProgram> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Department)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Requirements)
                .HasMaxLength(2000);

            builder.HasMany(x => x.Applications)
                .WithOne(x => x.TrainingProgram)
                .HasForeignKey(x => x.TrainingProgramId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔥 تحسين مهم جداً
            builder.Property(x => x.Status)
                .HasConversion<int>(); // optional لكن مهم للثبات
        }
    }
}
