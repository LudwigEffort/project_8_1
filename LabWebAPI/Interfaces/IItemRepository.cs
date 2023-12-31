using LabWebAPI.Controllers;
using LabWebAPI.Dto;
using LabWebAPI.Model;

namespace LabWebAPI.Interfaces
{
    public interface IItemRepository
    {
        //* Read Methods
        ICollection<Item> GetItems();
        Item GetItemById(int itemId);

        //* Create Methods
        bool CreateItem(Item item, List<SoftwareDto> softwares);

        //* Update Method
        bool UpdateItem(Item item, List<SoftwareDto> softwares);

        //* Delete Method
        bool DeleteItem(Item item);

        //* Utils Method
        bool ItemExists(int itemId);
        bool Save();
        Item CheckItemIfExists(ItemDto itemCreate);
        Item CheckItemIfExists(ItemPostDto itemCreate);
    }
}