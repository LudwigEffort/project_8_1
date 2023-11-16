using LabWebAPI.Model;

namespace LabWebAPI.Data
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
            if (!_dataContext.Items.Any())
            {

                //? Software
                var software1 = new Software { SoftwareName = "Arch Linux" };
                var software2 = new Software { SoftwareName = "Windows 10" };
                var software3 = new Software { SoftwareName = "Mac OS X" };
                var software4 = new Software { SoftwareName = "Visual Studio Code" };
                var software5 = new Software { SoftwareName = "Firefox" };

                //? Computers
                var item1 = new Item
                {
                    ItemName = "Computer 1",
                    ItemType = "computer",
                    Description = "A powerful computer with Linux",
                    TechSpec = "Intel i7, 16GB RAM, 512GB SSD",
                    ItemIdentifier = "PC-0000001",
                    Status = "available",
                    CreationDate = DateTime.Now,
                    ItemSoftwares = new List<ItemSoftware>
                    {
                        new ItemSoftware { Software = software1 },
                        new ItemSoftware { Software = software4 },
                        new ItemSoftware { Software = software5 },
                    }
                };
                var item2 = new Item
                {
                    ItemName = "Computer 2",
                    ItemType = "computer",
                    Description = "A powerful computer with Linux",
                    TechSpec = "Intel i7, 16GB RAM, 512GB SSD",
                    ItemIdentifier = "PC-0000002",
                    Status = "available",
                    CreationDate = DateTime.Now,
                    ItemSoftwares = new List<ItemSoftware>
                    {
                        new ItemSoftware { Software = software1 },
                        new ItemSoftware { Software = software4 },
                        new ItemSoftware { Software = software5 },
                    }
                };
                var item3 = new Item
                {
                    ItemName = "Computer 3",
                    ItemType = "computer",
                    Description = "A powerful computer with Linux",
                    TechSpec = "Intel i7, 16GB RAM, 512GB SSD",
                    ItemIdentifier = "PC-0000003",
                    Status = "available",
                    CreationDate = DateTime.Now,
                    ItemSoftwares = new List<ItemSoftware>
                    {
                        new ItemSoftware { Software = software1 },
                        new ItemSoftware { Software = software4 },
                        new ItemSoftware { Software = software5 },
                    }
                };
                var item4 = new Item
                {
                    ItemName = "Computer 4",
                    ItemType = "copmputer",
                    Description = "A powerful computer with Linux",
                    TechSpec = "Intel i7, 16GB RAM, 512GB SSD",
                    ItemIdentifier = "PC-0000004",
                    Status = "available",
                    CreationDate = DateTime.Now,
                    ItemSoftwares = new List<ItemSoftware>
                    {
                        new ItemSoftware { Software = software1 },
                        new ItemSoftware { Software = software4 },
                        new ItemSoftware { Software = software5 },
                    }
                };
                var item5 = new Item
                {
                    ItemName = "Computer 5",
                    ItemType = "copmputer",
                    Description = "A powerful computer with Linux",
                    TechSpec = "Intel i7, 16GB RAM, 512GB SSD",
                    ItemIdentifier = "PC-0000005",
                    Status = "available",
                    CreationDate = DateTime.Now,
                    ItemSoftwares = new List<ItemSoftware>
                    {
                            new ItemSoftware { Software = software1 },
                            new ItemSoftware { Software = software4 },
                            new ItemSoftware { Software = software5 },
                    }
                };

                //? Lim
                var item6 = new Item
                {
                    ItemName = "Lim 1",
                    ItemType = "lim",
                    Description = "Interactive Whiteboard",
                    ItemIdentifier = "LIM-000001",
                    Status = "available",
                    CreationDate = DateTime.Now,
                };
                var item7 = new Item
                {
                    ItemName = "Lim 2",
                    ItemType = "lim",
                    Description = "Interactive Whiteboard",
                    ItemIdentifier = "LIM-000002",
                    Status = "available",
                    CreationDate = DateTime.Now,
                };
                var item8 = new Item
                {
                    ItemName = "Lim 3",
                    ItemType = "lim",
                    Description = "Interactive Whiteboard",
                    ItemIdentifier = "LIM-000003",
                    Status = "available",
                    CreationDate = DateTime.Now,
                };

                //? Lab Users
                var labUser1 = new LabUser
                {
                    FirstName = "Robert",
                    LastName = "Oppenheimer",
                    EmailAddress = "client1@example.com",
                    PhoneNumber = "654321098",
                    Role = "admin",
                    Token = "token_1"
                };
                var labUser2 = new LabUser
                {
                    FirstName = "Richard",
                    LastName = "Feynman",
                    EmailAddress = "client2@example.com",
                    PhoneNumber = "765432109",
                    Role = "client",
                    Token = "token_2"
                };
                var labUser3 = new LabUser
                {
                    FirstName = "Enrico",
                    LastName = "Fermi",
                    EmailAddress = "client3@example.com",
                    PhoneNumber = "876543210",
                    Role = "client",
                    Token = "token_3"
                };
                var labUser4 = new LabUser
                {
                    FirstName = "Niels",
                    LastName = "Bihr",
                    EmailAddress = "client4@example.com",
                    PhoneNumber = "987654321",
                    Role = "client",
                    Token = "token_4"
                };
                var labUser5 = new LabUser
                {
                    FirstName = "Ernest",
                    LastName = "Lawrence",
                    EmailAddress = "client5@example.com",
                    PhoneNumber = "098765432",
                    Role = "client",
                    Token = "token_5"
                };

                //? Rservations
                var reservation1 = new Reservation
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    ReservationStatus = "Active",
                    ItemId = item1.Id,
                    LabUserId = labUser2.Id,
                    Item = item1,
                    LabUser = labUser2
                };
                var reservation2 = new Reservation
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2),
                    ReservationStatus = "Active",
                    ItemId = item2.Id,
                    LabUserId = labUser3.Id,
                    Item = item2,
                    LabUser = labUser3
                };

                //? Rooms
                var room1 = new Room
                {
                    RoomName = "Room 101",
                    Items = new List<Item> {
                        item1,
                        item2,
                        item3,
                        item4,
                        item5,
                        item6
                    }
                };
                var room2 = new Room
                {
                    RoomName = "Room 111",
                    Items = new List<Item> { item7 }
                };
                var room3 = new Room
                {
                    RoomName = "Room 76",
                    Items = new List<Item> { item8 }
                };

                //? DataContext
                _dataContext.Items.AddRange(new[] {
                    item1,
                    item2,
                    item3,
                    item4,
                    item5,
                    item6
                });
                _dataContext.LabUsers.AddRange(new[] {
                    labUser1,
                    labUser2,
                    labUser3,
                    labUser4,
                    labUser5
                });
                _dataContext.Reservations.AddRange(new[] { reservation1, reservation2 });
                _dataContext.Rooms.AddRange(new[] { room1, room2, room3 });
                _dataContext.Softwares.AddRange(new[] {
                    software1,
                    software2,
                    software3,
                    software4,
                    software5
                });
                _dataContext.SaveChanges();
            }
        }
    }
}