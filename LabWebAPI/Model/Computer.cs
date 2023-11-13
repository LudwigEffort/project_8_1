namespace LabWebAPI.Model
{
    public class Computer
    {
        public int Id { get; set; }
        public string TechSpec { get; set; }
        public Item Item { get; set; } //? One (to one with Item)
        public ICollection<ComputerSoftware> ComputerSoftwares { get; set; } //? Many (to Manu with Software)
    }
}