using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Queries;

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
            var seats = await _eventRepository.GetSeatsByEventAndSectorAsync(query.EventId, query.SectorId);

            return seats.Select(seat => new SeatResponse
            {
                Id = seat.Id,
                SeatNumber = seat.SeatNumber,
                Status = seat.Status
            }).ToList();
        }
    }
}
