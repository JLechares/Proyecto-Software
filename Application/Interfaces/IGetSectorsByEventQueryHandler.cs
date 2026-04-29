using Application.DTOs;
using Application.UseCases.Events.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IGetSectorsByEventQueryHandler
    {
        Task<IEnumerable<SectorResponse>> HandleAsync(GetSectorsByEventQuery query);
    }
}
