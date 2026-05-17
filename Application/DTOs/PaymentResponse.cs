using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class PaymentResponse
    {
        public Guid ReservationId { get; set; }
        public required string ReservationStatus { get; set; }
        public Guid SeatId { get; set; }
        public required string SeatStatus { get; set; } 
        public DateTime PaidAt { get; set; }

    }
}
