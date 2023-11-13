namespace LabWebAPI.Model
{
    public class ComputerSoftware
    {
        public int ComputerId { get; set; }
        public int SoftwareId { get; set; }
        public Computer Computer { get; set; }
        public Software Software { get; set; }
    }
}