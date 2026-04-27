using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class RESERVATION
    {
        public int UserId { get; set; }

        public Guid SeatId { get; set; }

        public required string Status { get; set; }

        public DateTime ReservedAt { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
