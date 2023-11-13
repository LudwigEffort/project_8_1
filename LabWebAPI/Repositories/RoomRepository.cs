using LabWebAPI.Data;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;

namespace LabWebAPI.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRoom(Room room)
        {
            _context.Add(room);
            return Save();
        }

        public bool DeleteRoom(Room room)
        {
            _context.Remove(room);
            return Save();
        }

        public Room GetRoomById(int id)
        {
            return _context.Rooms.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Room> GetRooms()
        {
            return _context.Rooms.ToList();
        }

        public bool RoomExsits(int id)
        {
            return _context.Rooms.Any(r => r.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRoom(Room room)
        {
            _context.Update(room);
            return Save();
        }
    }
}