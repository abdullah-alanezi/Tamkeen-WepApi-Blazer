using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Common.BaseEntity;
using Tamkeen.Domain.Enums;

namespace Tamkeen.Domain.Entities
{
    public class TrainingProgram : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty; // المتطلبات
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public TrainingProgramStatus Status { get; set; }

        // علاقة: البرنامج الواحد يحتوي على طلبات تقديم كثيرة
        public ICollection<TrainingApplication> Applications { get; set; } = new List<TrainingApplication>();
    }
}
