using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class SECTOR
    {
        [Key] // Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incrementing ID
        public int Id { get; set; }
        public int EventId { get; set; }

        [ForeignKey("EventId")] // Foreign key to the EVENT table
        public virtual EVENT Event { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }
        public virtual ICollection<SEAT> Seats { get; set; }
    }
}
