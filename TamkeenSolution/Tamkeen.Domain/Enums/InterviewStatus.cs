using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Domain.Enums
{
    public enum InterviewStatus
    {
        Scheduled = 1,  // تم تحديد موعد المقابلة
        Completed = 2,  // تمت المقابلة بنجاح
        Canceled = 3,   // تم إلغاء الموعد
        NoShow = 4      // لم يحضر المتقدم للمقابلة
    }
}
