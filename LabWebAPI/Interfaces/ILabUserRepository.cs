using LabWebAPI.Model;

namespace LabWebAPI.Interfaces
{
    public interface ILabUserRepository
    {
        //* Read Methods
        ICollection<LabUser> GetLabUsers();
        LabUser GetLabUserById(int id);

        //* Create Method
        bool CreateLabUser(LabUser labUser);

        //* Update Method
        bool UpdateLabUser(LabUser labUser);

        //* Delete Method
        bool DeleteLabUser(LabUser labUser);

        //* Utils Methods
        bool LabUserExists(int id);
        bool Save();
    }
}