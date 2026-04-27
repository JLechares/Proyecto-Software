using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class RESERVATION
    {
        [Key]
        public Guid Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual USER User { get; set; }

        public Guid SeatId { get; set; }
        [ForeignKey("SeatId")]
        public virtual SEAT Seat { get; set; }

        public required string Status { get; set; }

        public DateTime ReservedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        
    }
}
