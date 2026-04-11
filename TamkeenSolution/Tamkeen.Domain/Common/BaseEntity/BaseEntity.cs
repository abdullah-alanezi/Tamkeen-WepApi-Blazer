using System;
using System.Collections.Generic;
using System.Text;

namespace Tamkeen.Domain.Common.BaseEntity
{
    public abstract class BaseEntity
    {
        // تغيير النوع إلى Guid
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
