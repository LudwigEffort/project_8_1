namespace LoginWebAPI.Dto
{
    public class UserAdminDto
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBanned { get; set; }
    }
}