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
        bool CreateItem(int softwareId, Item item);

        //* Update Method
        bool UpdateItem(int softwareId, Item item);

        //* Delete Method
        bool DeleteItem(Item item);

        //* Utils Method
        bool ItemExists(int itemId);
        bool Save();
        Item CheckItemIfExists(ItemDto itemCreate);
    }
}