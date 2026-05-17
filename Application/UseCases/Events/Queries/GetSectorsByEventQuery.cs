using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Queries
{
    public class GetSectorsByEventQuery
    {
        public int EventId { get; set; }
        public GetSectorsByEventQuery(int eventId)
        {
            EventId = eventId;
        }
    }
}
