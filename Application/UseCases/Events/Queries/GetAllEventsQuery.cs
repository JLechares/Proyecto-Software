using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Queries
{
    public class GetAllEventsQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}
