using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class EVENT
    {
        public required string Name { get; set; }

        public DateTime EventDate { get; set; }

        public required string Venue { get; set; }

        public required string Status { get; set; }
    }
}
