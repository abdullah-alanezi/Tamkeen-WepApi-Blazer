using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities.Evaluations;

namespace Tamkeen.Persistence.Configurations
{
    public class MonthlyEvaluationConfiguration : IEntityTypeConfiguration<MonthlyEvaluation>
    {
        public void Configure(EntityTypeBuilder<MonthlyEvaluation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Comments)
                .HasMaxLength(1000);

            builder.HasOne(x => x.TrainingApplication)
                .WithMany(x => x.MonthlyEvaluations)
                .HasForeignKey(x => x.TrainingApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
            // ✔ OK لأنها مرتبطة مباشرة بالطلب

            builder.HasIndex(x => new { x.TrainingApplicationId, x.Month, x.Year })
                .IsUnique();
        }
    }
}
