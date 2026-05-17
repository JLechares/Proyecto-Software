namespace Domain.Entities
{
    public class EVENT
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public DateTime EventDate { get; set; }

        public required string Venue { get; set; }

        public required string Status { get; set; }

        public virtual ICollection<SECTOR> Sectors { get; set; } = new List<SECTOR>();
    }
}
