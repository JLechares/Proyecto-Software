using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Handlers
{
    public class GetAllEventsHandler : IGetAllEventsQueryHandler
    {
        public Task<IEnumerable<EventResponse>> HandleAsync()
        {
            throw new NotImplementedException();
        }
    }
}
