using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Events.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Software.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReserveSeatCommandHandler _reserveSeatHandler;
        public ReservationsController(IReserveSeatCommandHandler reserveSeatHandler)
        {
            _reserveSeatHandler = reserveSeatHandler;
        }
        [HttpPost("v1/reservations")]
        public async Task<IActionResult> ReserveSeat([FromBody] ReserveSeatResponse response)
        {
            try
            {
                var command = new ReserveSeatCommand
                {
                    SeatId = response.SeatId,
                    UserId = response.UserId,
                };

                var result = await _reserveSeatHandler.HandleAsync(command);

                return StatusCode(201, result);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409, new
                {
                    message = "Lo sentimos, el asiento fue seleccionado por otro usuario hace instantes. Por favor, elegí otro."
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
