using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Application.DTOs
{
    public class ReserveSeatResponse
    {
        public Guid ReservationId { get; set; }
        public int UserId { get; set; }
        public Guid SeatId { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
