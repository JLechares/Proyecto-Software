using Application.Interfaces;
using Application.UseCases.Payments.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Software.Controllers
{
    [ApiController]
    [Route("api/v1/reservations/{reservationId}/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IProcessPaymentHandler _processPaymentHandler;

        public PaymentsController(IProcessPaymentHandler processPaymentHandler)
        {
            _processPaymentHandler = processPaymentHandler;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(Guid reservationId)
        {
            try
            {
                var command = new ProcessPaymentCommand
                {
                    ReservationId = reservationId
                };

                var result = await _processPaymentHandler.HandleAsync(command);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}