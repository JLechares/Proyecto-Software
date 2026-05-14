using Application.DTOs;
using Application.Interfaces;   
using Application.UseCases.Events.Commands;
using Application.UseCases.Events.Queries;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Software.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IGetAllEventsQueryHandler _getAllEventsHandler;
        private readonly IGetSeatsStatusQueryHandler _getSeatsStatusHandler;
        private readonly IGetSectorsByEventQueryHandler _getSectorsByEventHandler;
        private readonly IReserveSeatCommandHandler _reserveSeatHandler;

        public EventsController(
            IGetAllEventsQueryHandler getAllEventsHandler,
            IGetSeatsStatusQueryHandler getSeatsStatusHandler,
            IGetSectorsByEventQueryHandler getSectorsByEventHandler,
            IReserveSeatCommandHandler reserveSeatHandler)
        {
            _getAllEventsHandler = getAllEventsHandler;
            _getSeatsStatusHandler = getSeatsStatusHandler;
            _getSectorsByEventHandler = getSectorsByEventHandler;
            _reserveSeatHandler = reserveSeatHandler;
        }

        [HttpGet("v1/events")]
        public async Task<IActionResult> GetAllEvents()
        {
            var query = new GetAllEventsQuery();
            var events = await _getAllEventsHandler.HandleAsync(query);
            return Ok(events);
        }

        [HttpGet("v1/{eventId}/sectors")]
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

        [HttpPost("reserve-seat")]
        public async Task<IActionResult> ReserveSeat([FromBody] ReserveSeatResponse response)
        {
            try
            {
                var command = new ReserveSeatCommand
                {
                    SeatId = response.SeatId,
                    UserId = response.UserId
                };

                var result = await _reserveSeatHandler.HandleAsync(command);

                return StatusCode(201, result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
