using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class SEAT
    {
        public int SectorId { get; set; }

        public required string RowIdentifier { get; set; }

        public int SeatNumber { get; set; }

        public required string Status { get; set; }
        public int Version { get; set; }
    }
}
