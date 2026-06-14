using Application.Interfaces;   
using Application.UseCases.Events.Queries;
using Microsoft.AspNetCore.Mvc;


namespace Proyecto_Software.Controllers
{
    [Route("api/v1/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IGetAllEventsQueryHandler _getAllEventsHandler;
        private readonly IGetSeatsStatusQueryHandler _getSeatsStatusHandler;
        private readonly IGetSectorsByEventQueryHandler _getSectorsByEventHandler;

        public EventsController
            (
            IGetAllEventsQueryHandler getAllEventsHandler,
            IGetSeatsStatusQueryHandler getSeatsStatusHandler,
            IGetSectorsByEventQueryHandler getSectorsByEventHandler
            )
        {
            _getAllEventsHandler = getAllEventsHandler;
            _getSeatsStatusHandler = getSeatsStatusHandler;
            _getSectorsByEventHandler = getSectorsByEventHandler;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents([FromQuery] GetAllEventsQuery query)
        {
            var events = await _getAllEventsHandler.HandleAsync(query);
            return Ok(events);
        }

        [HttpGet("{eventId}/sectors")]
        public async Task<IActionResult> GetSectors(int eventId)
        {
            var query = new GetSectorsByEventQuery(eventId);

            var result = await _getSectorsByEventHandler.HandleAsync(query);

            return Ok(result);
        }

        [HttpGet("{eventId}/sectors/{sectorId}/seats")]
        public async Task<IActionResult> GetSeats(int eventId, int sectorId)
        {
            var query = new GetSeatsStatusQuery(eventId, sectorId);
            var result = await _getSeatsStatusHandler.HandleAsync(query);

            if (result == null || !result.Any())
                return NotFound($"No se encontraron asientos para el sector {sectorId} en el evento {eventId}");

            return Ok(result);
        }
        
    }
}
