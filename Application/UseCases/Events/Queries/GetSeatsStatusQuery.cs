using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.Events.Queries
{
    public class GetSeatsStatusQuery
    {
        public int EventId { get; set; }
        public int SectorId { get; set; }

        public GetSeatsStatusQuery(int eventId, int sectorId)
        {
            EventId = eventId;
            SectorId = sectorId;
        }
    }
}
