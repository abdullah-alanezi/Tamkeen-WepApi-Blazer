using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Domain.Enums
{
    public enum ApplicationStatus
    {
        Pending = 1,      // قيد الانتظار
        Shortlisted = 2,  // مرشح للمقابلة
        Accepted = 3,     // مقبول
        Rejected = 4,     // مرفوض
        Withdrawn = 5     // منسحب
    }
}
