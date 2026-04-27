using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class AUDIT_LOG
    {
        [Key] // Primary key
        public Guid Id { get; set; }
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual USER? User { get; set; }

        public required string Action { get; set; }

        public required string EntityType { get; set; }

        public required string EntityId { get; set; }
        public required string Details { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
