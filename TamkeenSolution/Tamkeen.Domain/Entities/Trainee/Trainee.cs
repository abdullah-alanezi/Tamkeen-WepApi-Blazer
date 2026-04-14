using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Common.BaseEntity;

namespace Tamkeen.Domain.Entities.Trainee
{
    public class Trainee : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string Major { get; set; } = string.Empty;
        public string? ResumeUrl { get; set; }

        // ربط المستخدم بالـ Identity لاحقاً
        public string? IdentityUserId { get; set; }

        // علاقة: المتدرب يمكنه التقديم على أكثر من برنامج
        public ICollection<TrainingApplication> AppliedPrograms { get; set; } = new List<TrainingApplication>();
    }
}
