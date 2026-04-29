using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class EventResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public required string Venue { get; set; }
        public required string Status { get; set; }
    }
}
