using Application.DTOs;
using Application.UseCases.Payments.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IProcessPaymentHandler
    {
        Task<PaymentResponse> HandleAsync(ProcessPaymentCommand command);
    }
}
