using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Handlers
{
    public class GetSectorsByEventHandler : IGetSectorsByEventQueryHandler
    {
        private readonly IEventRepository _eventRepository;
        public GetSectorsByEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<IEnumerable<SectorResponse>> HandleAsync(GetSectorsByEventQuery query)
        {
            var sectors = await _eventRepository.GetSectorsByEventAsync(query.EventId);

            return sectors.Select(s => new SectorResponse
            {
                Id = s.Id,
                EventId = s.EventId,
                Name = s.Name,
                Capacity = s.Capacity
            });
        }
    }
}
