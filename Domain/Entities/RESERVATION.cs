using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class RESERVATION
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }

        public required virtual USER User { get; set; }

        public Guid SeatId { get; set; }
        public required virtual SEAT Seat { get; set; }

        public required string Status { get; set; }

        public DateTime ReservedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

    }
}
