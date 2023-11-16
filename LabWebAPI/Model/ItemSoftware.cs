namespace LabWebAPI.Model
{
    public class ItemSoftware
    {
        public int ItemId { get; set; }
        public int SoftwareId { get; set; }
        public Item Item { get; set; }
        public Software Software { get; set; }
    }
}