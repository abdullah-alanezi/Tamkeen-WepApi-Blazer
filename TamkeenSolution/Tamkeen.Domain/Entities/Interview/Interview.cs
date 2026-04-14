using Tamkeen.Domain.Common.BaseEntity;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Domain.Enums;

namespace Tamkeen.Domain.Entities.Interview
{
    public class Interview : BaseEntity
    {
        // ربط المقابلة بطلب تقديم محدد
        public Guid TrainingApplicationId { get; set; }

        public DateTime ScheduledOn { get; set; } // موعد المقابلة
        public string? MeetingLink { get; set; } // رابط (Teams/Zoom) إذا كانت عن بعد
        public string? Location { get; set; }    // مكان المقابلة إذا كانت حضورية
        public string? InterviewerName { get; set; } // اسم الشخص الذي سيجري المقابلة

        public string? Feedback { get; set; }    // ملاحظات المقابل بعد الانتهاء
        public InterviewStatus Status { get; set; } = InterviewStatus.Scheduled;

        // Navigation Property
        public TrainingApplication TrainingApplication { get; set; } = null!;
    }
}
