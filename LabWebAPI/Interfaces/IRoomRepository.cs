using LabWebAPI.Model;

namespace LabWebAPI.Interfaces
{
    public interface IRoomRepository
    {
        //* Read Methods
        ICollection<Room> GetRooms();
        Room GetRoomById(int id);

        //TODO: get item by room
        //ICollection<Item> GetItemsByRoom(int roomId);

        //* Create Method
        bool CreateRoom(Room room);

        //* Update Method
        bool UpdateRoom(Room room);

        //* Delete Method
        bool DeleteRoom(Room room);

        //* Utils Method
        bool RoomExsits(int id);
        bool Save();
    }
}