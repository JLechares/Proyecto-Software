using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Handlers
{
    public class GetSeatsStatusHandler : IGetSeatsStatusQueryHandler
    {
        public Task<SeatMapResponse> HandleAsync(int sectorId)
        {
            throw new NotImplementedException();
        }
    }
}
