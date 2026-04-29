using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Handlers
{
    public class ReserveSeatHandler : IReserveSeatCommandHandler
    {
        private readonly IEventRepository _eventRepository;
        public ReserveSeatHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public Task<CommandResult> HandleAsync(ReserveSeatCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
