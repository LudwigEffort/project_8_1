namespace LabWebAPI.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; } = "empty";
        public string Description { get; set; }
        public string TechSpec { get; set; } = "empty";
        public string ItemIdentifier { get; set; }
        public string Status { get; set; } = "available";
        //public DateTime CreationDate { get; set; }
        public int RoomId { get; set; }
        public List<SoftwareDto> Softwares { get; set; }
    }
}