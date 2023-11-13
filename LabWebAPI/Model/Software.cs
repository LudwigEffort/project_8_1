namespace LabWebAPI.Model
{
    public class Software
    {
        public int Id { get; set; }
        public string SoftwareName { get; set; }
        public ICollection<ComputerSoftware> ComputerSoftwares { get; set; } //? Many (to Many with Computer)
    }
}