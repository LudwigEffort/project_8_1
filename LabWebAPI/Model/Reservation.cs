namespace LabWebAPI.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ReservationStatus { get; set; }
        public int ItemIt { get; set; } //? item foregin key 
        public int LabUserId { get; set; } //? lab user foregin key
        public Item Item { get; set; } //? Onet (to many with Item)
        public LabUser LabUser { get; set; } //? One (to many with LabUser)
    }
}