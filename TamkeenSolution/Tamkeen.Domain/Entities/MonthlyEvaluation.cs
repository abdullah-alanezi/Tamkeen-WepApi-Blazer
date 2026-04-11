using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Common.BaseEntity;

namespace Tamkeen.Domain.Entities
{
    public class MonthlyEvaluation : BaseEntity
    {
        // ربط التقييم بطلب تقديم محدد (المتدرب المقبول في برنامج معين)
        public Guid TrainingApplicationId { get; set; }

        public int Month { get; set; } // رقم الشهر (1-12)
        public int Year { get; set; }  // السنة الميلادية

        public int AttendanceGrade { get; set; }  // درجة الحضور والالتزام
        public int PerformanceGrade { get; set; } // درجة الأداء والمهام التقنية
        public string? Comments { get; set; }      // ملاحظات المشرف أو المدير

        // خاصية التنقل (Navigation Property)
        public virtual TrainingApplication TrainingApplication { get; set; } = null!;
    }
}
