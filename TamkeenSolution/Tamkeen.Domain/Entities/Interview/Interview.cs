using Tamkeen.Domain.Common.BaseEntity;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Domain.Enums;

namespace Tamkeen.Domain.Entities.Interview
{
    public class Interview : BaseEntity
    {
        public Guid TrainingApplicationId { get; set; }

        public DateTime ScheduledOn { get; set; }

        public string? MeetingLink { get; set; }
        public string? Location { get; set; }

        public string? InterviewerName { get; set; }

        public string? Feedback { get; set; }

        public InterviewStatus Status { get; set; } = InterviewStatus.Scheduled;

        public TrainingApplication TrainingApplication { get; set; } = null!;
    }
}
