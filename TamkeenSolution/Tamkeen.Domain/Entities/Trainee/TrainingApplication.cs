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
        public Guid TrainingProgramId { get; set; }
        public Guid TraineeId { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

        public string? HRFeedback { get; set; }

        public TrainingProgram TrainingProgram { get; set; } = null!;
        public Trainee Trainee { get; set; } = null!;

        public ICollection<Interview.Interview> Interviews { get; set; }
            = new List<Interview.Interview>();

        public ICollection<MonthlyEvaluation> MonthlyEvaluations { get; set; }
            = new List<MonthlyEvaluation>();
    }
}
