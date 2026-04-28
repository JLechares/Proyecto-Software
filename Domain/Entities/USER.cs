using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class USER
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public virtual ICollection<AUDIT_LOG> AuditLogs { get; set; } = new List<AUDIT_LOG>();
        public virtual ICollection<RESERVATION> Reservations { get; set; } = new List<RESERVATION>();
    }
}
