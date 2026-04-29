using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IGetSectorsByEventQueryHandler
    {
        Task<IEnumerable<SectorResponse>> HandleAsync(int eventId);
    }
}
