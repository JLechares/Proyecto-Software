using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SECTOR
    {
        public int EventId { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }
    }
}
