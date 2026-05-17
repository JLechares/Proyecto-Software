using Application.DTOs;
using Application.UseCases.Events.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IGetSeatsStatusQueryHandler
    {
        Task<IEnumerable<SeatResponse>> HandleAsync(GetSeatsStatusQuery query);
    }
}
