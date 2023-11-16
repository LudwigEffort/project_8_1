using System.ComponentModel.DataAnnotations;

namespace LabWebAPI.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; } = "empty";
        public string Description { get; set; } = "empty";
        public string TechSpec { get; set; } = "empty";
        [StringLength(10, ErrorMessage = "The Item Identifier must be at leadt {2} characters long.", MinimumLength = 10)]
        public string ItemIdentifier { get; set; }
        public string Status { get; set; } = "available";
        public DateTime CreationDate { get; set; }
        public int RoomId { get; set; } //? room foregin key
        public Room Room { get; set; } //? One (to many with Room)
        public ICollection<Reservation> Reservations { get; set; } //? Many (to one with Reservation)
        public ICollection<ItemSoftware> ItemSoftwares { get; set; } //? Many (to Manu with Software)
    }
}