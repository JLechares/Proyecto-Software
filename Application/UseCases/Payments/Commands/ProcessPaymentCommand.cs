using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Payments.Commands
{
    public class ProcessPaymentCommand
    {
        public Guid ReservationId { get; set; }
    }
}
