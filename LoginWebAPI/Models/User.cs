using System.ComponentModel.DataAnnotations;

namespace LoginWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100,
        ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
        ErrorMessage = "The password must contain at least one uppercase letter, one lowercase letter, and a number.")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } = "client";

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public bool IsBanned { get; set; } = false;

        public DateTime CreationTime { get; set; }
    }
}