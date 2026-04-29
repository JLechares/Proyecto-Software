using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Handlers
{
    public class GetSeatsStatusHandler : IGetSeatsStatusQueryHandler
    {
        private readonly IEventRepository _eventRepository;
        public GetSeatsStatusHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<SeatResponse>> HandleAsync(GetSeatsStatusQuery query)
        {
            var seats = await _eventRepository.GetSeatsBySectorIdAsync(query.SectorId);

            return seats.Select(s => new SeatResponse
            {
                Id = s.Id,
                SectorId = s.SectorId,
                RowIdentifier = s.RowIdentifier,
                SeatNumber = s.SeatNumber,
                Status = s.Status.ToString(),
                version = s.Version
            });
        }
    }
}
