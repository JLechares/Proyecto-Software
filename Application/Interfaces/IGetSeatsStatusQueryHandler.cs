using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IGetSeatsStatusQueryHandler
    {
        Task<SeatMapResponse> HandleAsync(int sectorId);
    }
}
