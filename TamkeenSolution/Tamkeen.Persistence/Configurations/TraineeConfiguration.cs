using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Persistence.Configurations
{
    public class TraineeConfiguration : IEntityTypeConfiguration<Trainee>
    {
        public void Configure(EntityTypeBuilder<Trainee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);

            // جعل الإيميل فريداً ومطلوباً
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.University).HasMaxLength(200);
        }
    }
}
