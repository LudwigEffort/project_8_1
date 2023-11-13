namespace LabWebAPI.Model
{
    public class LabUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; } = "client";
        public string Token { get; set; }
        public ICollection<Reservation> Reservations { get; set; } //? Many (to one with reservation)
    }
}