using Application.DTOs;
using Application.UseCases.Events.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IReserveSeatCommandHandler
    {
        Task<CommandResult> HandleAsync(ReserveSeatCommand command);
    }
}
