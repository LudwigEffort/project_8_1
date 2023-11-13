using LoginWebAPI.Data;
using LoginWebAPI.Interfaces;
using LoginWebAPI.Models;

namespace LoginWebAPI.Repository
{
    public class UserClientRepository : IUserClientRepository
    {
        private readonly DataContext _context;

        public UserClientRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.EmailAddress == email).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExists(User user)
        {
            _context.Update(user);
            return Save();
        }
    }
}