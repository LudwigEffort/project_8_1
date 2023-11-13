using System.ComponentModel.DataAnnotations;

namespace LabWebAPI.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        [StringLength(10, ErrorMessage = "The Item Identifier must be at leadt {2} characters long.", MinimumLength = 10)]
        public string ItemIdentifier { get; set; }
        public string Status { get; set; } = "available";
        public DateTime CreationDate { get; set; }
        public int RoomId { get; set; } //? room foregin key
        public Room Room { get; set; } //? One (to many with Room)
        public Computer Computer { get; set; } //? One (to one with Computer)
        public ICollection<Reservation> Reservations { get; set; } //? Many (to one with Reservation)
    }
}