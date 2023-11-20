namespace LabWebAPI.Dto
{
    public class ReservationPostDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        // public DateTime EndTime { get; set; }
        public string ReservationStatus { get; set; }
        public int ItemId { get; set; }
        public int LabUserId { get; set; }
        // public ItemDto Item { get; set; }
        // public LabUserDto LabUser { get; set; }
    }
}