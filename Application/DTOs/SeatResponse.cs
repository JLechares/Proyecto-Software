using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class SeatResponse
    {
        public Guid Id { get; set; }
        public int SectorId { get; set; }
        public required string RowIdentifier { get; set; }
        public int SeatNumber { get; set; }
        public required string Status { get; set; }
        public int version { get; set; }  
    }
}
