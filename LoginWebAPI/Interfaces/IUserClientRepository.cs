using LoginWebAPI.Models;

namespace LoginWebAPI.Interfaces
{
    public interface IUserClientRepository
    {
        //* Read methods
        ICollection<User> GetUsers();
        User GetUserByEmail(string email);

        //* Create method
        bool CreateUser(User user);

        //* Utils methods
        bool UserExists(User user); //BUG: it had to be -> UserExists(int id)
        bool Save();
    }
}