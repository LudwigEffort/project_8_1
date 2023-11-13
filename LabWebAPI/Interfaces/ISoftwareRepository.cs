using LabWebAPI.Model;

namespace LabWebAPI.Interfaces
{
    public interface ISoftwareRepository
    {
        //* Read Methods
        ICollection<Software> GetSoftwares();
        Software GetSoftwareById(int id);

        //* Create Method
        bool CreateSoftware(Software software);

        //* Update Method
        bool UpdateSoftware(Software software);

        //* Delete Method
        bool DeleteSoftware(Software software);

        //* Utils Methods
        bool SoftwareExists(int id);
        bool Save();
    }
}