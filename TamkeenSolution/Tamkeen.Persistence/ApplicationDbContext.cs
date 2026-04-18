using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // تحديث التواريخ للكيانات التي ترث من BaseEntity
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        // TODO: إضافة CreatedBy عند ربط نظام الهوية
                        // entry.Entity.CreatedBy = _currentUserService?.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.UtcNow;
                        // TODO: إضافة ModifiedBy عند ربط نظام الهوية
                        // entry.Entity.ModifiedBy = _currentUserService?.UserId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}