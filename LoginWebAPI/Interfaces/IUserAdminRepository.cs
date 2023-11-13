using LoginWebAPI.Models;

namespace LoginWebAPI.Interfaces
{
    public interface IUserAdminRepository
    {
        //* Read methods
        ICollection<User> GetUsers();
        User GetUser(int id);

        //* Create method
        bool CreateUser(User user);

        //* Update method
        bool UpdateUser(User user);

        //* Delite method
        bool DeliteUser(User user);

        //* Utils methods
        bool UserExists(int id);
        bool Save();
    }
}