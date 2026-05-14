using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace Application.DTOs
{
    public class ReserveSeatResponse
    {
        public int UserId { get; set; }
        public Guid SeatId { get; set; }
        public Guid ReservationID { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
