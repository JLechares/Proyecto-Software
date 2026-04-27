using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class SEAT
    {
        [Key] // Primary key
        public Guid Id { get; set; }
        public int SectorId { get; set; }

        [ForeignKey("SectorId")] // Foreign key to the SECTOR table
        public virtual SECTOR Sector { get; set; }
        public required string RowIdentifier { get; set; }

        public int SeatNumber { get; set; }

        public required string Status { get; set; }
        public int Version { get; set; }

        public virtual ICollection<RESERVATION> Reservations { get; set; } = new List<RESERVATION>();
    }
}
