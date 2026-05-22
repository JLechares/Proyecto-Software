using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Application.UseCases.Payments.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_Software.Controllers
{
    [ApiController]
    [Route("api/v1/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IProcessPaymentHandler _processPaymentHandler;

        public PaymentsController(IProcessPaymentHandler processPaymentHandler)
        {
            _processPaymentHandler = processPaymentHandler;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentCommand command)
        {
            var result = await _processPaymentHandler.HandleAsync(command);
            return Ok(result);
        }
    }
}
