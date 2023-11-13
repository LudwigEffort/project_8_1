namespace LabWebAPI.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public ICollection<Item> Items { get; set; } //? Many (to one with Item)
    }
}