using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Handlers
{
    public class GetSectorsByEventHandler : IGetSectorsByEventQueryHandler
    {
        public Task<IEnumerable<SectorResponse>> HandleAsync(int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
