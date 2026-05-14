using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class PaymentResponse
    {
        public Guid ReservationId { get; set; }
        public string ReservationStatus { get; set; } = string.Empty;
        public Guid SeatId { get; set; }
        public string SeatStatus { get; set; } = string.Empty ;
        public DateTime PaidAt { get; set; }

    }
}
