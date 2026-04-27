using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AUDIT_LOG
    {
        public int? UserId { get; set; }

        public required string Action { get; set; }

        public required string EntityType { get; set; }

        public required string EntityId { get; set; }
        public required string Details { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
