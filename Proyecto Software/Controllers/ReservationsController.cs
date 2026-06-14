using Application.Interfaces;
using Application.UseCases.Events.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Software.Controllers
{
    [Route("api/v1/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReserveSeatCommandHandler _reserveSeatHandler;

        public ReservationsController(IReserveSeatCommandHandler reserveSeatHandler)
        {
            _reserveSeatHandler = reserveSeatHandler;
        }

        [HttpPost]
        public async Task<IActionResult> ReserveSeat([FromBody] ReserveSeatCommand command)
        {
            try
            {
                var result = await _reserveSeatHandler.HandleAsync(command);
                return StatusCode(201, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new
                {
                    message = "Lo sentimos, el asiento fue seleccionado por otro usuario hace instantes. Por favor, elegí otro."
                });
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