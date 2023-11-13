using LoginWebAPI.Models;

namespace LoginWebAPI.Data
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        public Seed(DataContext context)
        {
            _dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!_dataContext.Users.Any())
            {
                List<User> users = new List<User>()
                {
                    //? Admins
                    new User()
                    {
                        EmailAddress = "admin1@example.com",
                        Password = "Admin123",
                        Role = "admin",
                        FirstName = "Aldo",
                        LastName = "Baglio",
                        PhoneNumber = "123456789",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "admin2@example.com",
                        Password = "Admin123",
                        Role = "admin",
                        FirstName = "Giacomo",
                        LastName = "Poretti",
                        PhoneNumber = "234567890",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "admin3@example.com",
                        Password = "Admin123",
                        Role = "admin",
                        FirstName = "Giovanni",
                        LastName = "Storti",
                        PhoneNumber = "345678901",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    //? Clients
                    new User()
                    {
                        EmailAddress = "client1@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Robert",
                        LastName = "Oppenheimer",
                        PhoneNumber = "654321098",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client2@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Richard",
                        LastName = "Feynman",
                        PhoneNumber = "765432109",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client3@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Enrico",
                        LastName = "Fermi",
                        PhoneNumber = "876543210",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client4@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Niels",
                        LastName = "Bohr",
                        PhoneNumber = "987654321",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client5@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Ernest",
                        LastName = "Lawrence",
                        PhoneNumber = "098765432",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client6@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Isidor",
                        LastName = "Raibi",
                        PhoneNumber = "123451234",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client7@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Edward",
                        LastName = "Teller",
                        PhoneNumber = "76554315",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client8@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Albert",
                        LastName = "Einstein",
                        PhoneNumber = "543213210",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client9@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "Hans",
                        LastName = "Bethe",
                        PhoneNumber = "98765432",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    },
                    new User()
                    {
                        EmailAddress = "client10@example.com",
                        Password = "client123",
                        Role = "client",
                        FirstName = "David",
                        LastName = "Hill",
                        PhoneNumber = "13579023",
                        IsBanned = false,
                        CreationTime = DateTime.UtcNow
                    }
                };

                _dataContext.Users.AddRange(users);
                _dataContext.SaveChanges();
            }
        }
    }
}
