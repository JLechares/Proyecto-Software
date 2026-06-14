namespace Application.DTOs
{
    public class SeatResponse
    {
        public Guid Id { get; set; }
        public int SeatNumber { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
