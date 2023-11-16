using LabWebAPI.Data;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;

namespace LabWebAPI.Repositories
{
    public class LabUserRepository : ILabUserRepository
    {
        private readonly DataContext _context;

        public LabUserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateLabUser(LabUser labUser)
        {
            _context.Add(labUser);
            return Save();
        }

        public bool DeleteLabUser(LabUser labUser)
        {
            _context.Remove(labUser);
            return Save();
        }

        public LabUser GetLabUserByEmail(string email)
        {
            return _context.LabUsers.Where(u => u.EmailAddress == email).FirstOrDefault();
        }

        public LabUser GetLabUserById(int id)
        {
            return _context.LabUsers.Where(u => u.Id == id).FirstOrDefault();
        }

        public ICollection<LabUser> GetLabUsers()
        {
            return _context.LabUsers.ToList();
        }

        public bool LabUserExists(int id)
        {
            return _context.LabUsers.Any(u => u.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateLabUser(LabUser labUser)
        {
            _context.Update(labUser);
            return Save();
        }
    }
}