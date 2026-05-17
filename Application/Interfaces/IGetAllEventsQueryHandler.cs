using Application.DTOs;
using Application.UseCases.Events.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IGetAllEventsQueryHandler
    {
        Task<IEnumerable<EventResponse>> HandleAsync(GetAllEventsQuery query);
    }
}
