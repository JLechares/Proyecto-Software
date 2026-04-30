using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Commands
{
    public class ReserveSeatCommand
    {
        public Guid SeatId { get; set; }
        public int UserId { get; set; }
    }
}
