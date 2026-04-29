using Infraestructure.Persistence;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;   
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

        
    }
}
