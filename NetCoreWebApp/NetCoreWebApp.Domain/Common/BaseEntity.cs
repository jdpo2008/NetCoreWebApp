using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWebApp.Domain.Common
{
    public abstract class BaseEntity : AuditableBaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}
