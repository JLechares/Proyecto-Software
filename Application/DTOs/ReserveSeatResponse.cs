using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Application.DTOs
{
    public class ReserveSeatResponse
    {
        public int UserId { get; set; }
        public Guid SeatId { get; set; }
    }
}
