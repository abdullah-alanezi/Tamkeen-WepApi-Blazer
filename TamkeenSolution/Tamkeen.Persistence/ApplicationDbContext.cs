using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Common.BaseEntity;
using Tamkeen.Domain.Entities.Evaluations;
using Tamkeen.Domain.Entities.Interview;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<TrainingApplication> TrainingApplications { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<MonthlyEvaluation> MonthlyEvaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // تم التصحيح هنا
        {
            base.OnModelCreating(modelBuilder);

            // هذا السطر يخبر قاعدة البيانات أن تبحث عن أي ملف Configuration 
            // في هذا المشروع (Assembly) وتطبقه فوراً
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        // entry.Entity.CreatedBy = _currentUserService.UserId; // سنفعل هذا لاحقاً عند ربط الهوية
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
