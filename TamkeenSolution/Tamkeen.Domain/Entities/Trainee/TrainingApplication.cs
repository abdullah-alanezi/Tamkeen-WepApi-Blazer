using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Common.BaseEntity;
using Tamkeen.Domain.Entities.Evaluations;
using Tamkeen.Domain.Enums;

namespace Tamkeen.Domain.Entities.Trainee
{
    public class TrainingApplication : BaseEntity
    {
        // مفاتيح العلاقات
        public Guid TrainingProgramId { get; set; }
        public Guid TraineeId { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        public string? HRFeedback { get; set; } // ملاحظات الـ HR

        // Navigation Properties
        public TrainingProgram TrainingProgram { get; set; } = null!;
        public Trainee Trainee { get; set; } = null!;
        // داخل TrainingApplication.cs
        public ICollection<Tamkeen.Domain.Entities.Interview.Interview> Interviews { get; set; } = new List<Tamkeen.Domain.Entities.Interview.Interview>();
        public ICollection<MonthlyEvaluation> MonthlyEvaluations { get; set; } = new List<MonthlyEvaluation>();
    }
}
