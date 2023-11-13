using LabWebAPI.Data;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;

namespace LabWebAPI.Repositories
{
    public class SoftwareRepository : ISoftwareRepository
    {
        private readonly DataContext _context;

        public SoftwareRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSoftware(Software software)
        {
            _context.Add(software);
            return Save();
        }

        public bool DeleteSoftware(Software software)
        {
            _context.Remove(software);
            return Save();
        }

        public Software GetSoftwareById(int id)
        {
            return _context.Softwares.Where(s => s.Id == id).FirstOrDefault();
        }

        public ICollection<Software> GetSoftwares()
        {
            return _context.Softwares.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SoftwareExists(int id)
        {
            return _context.Softwares.Any(s => s.Id == id);
        }

        public bool UpdateSoftware(Software software)
        {
            _context.Update(software);
            return Save();
        }
    }
}