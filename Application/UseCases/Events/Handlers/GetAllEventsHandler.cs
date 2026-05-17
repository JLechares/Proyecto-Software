using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Handlers
{
    public class GetAllEventsHandler : IGetAllEventsQueryHandler
    {
        private readonly IEventRepository _eventRepository;

        public GetAllEventsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<IEnumerable<EventResponse>> HandleAsync(GetAllEventsQuery query)
        {
            var events = await _eventRepository.GetAllEventsAsync(query.Page, query.PageSize);
            return events.Select(e => new EventResponse
            {
                Id = e.Id,
                Name = e.Name,
                Date = e.EventDate,
                Venue = e.Venue,
                Status = e.Status
            });
        }
    }
}
