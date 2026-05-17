using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Queries
{
    public class GetAllEventsQuery
    {
        public int Page { get; } = 1;
        public int PageSize { get; } = 10;

    }
}
