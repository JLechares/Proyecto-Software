using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class EVENT
    {
        [Key] // Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrementing ID
        public int Id { get; set; }
        public required string Name { get; set; }

        public DateTime EventDate { get; set; }

        public required string Venue { get; set; }

        public required string Status { get; set; }

        public virtual ICollection<SECTOR> Sectors { get; set; } = new List<SECTOR>();
    }
}
