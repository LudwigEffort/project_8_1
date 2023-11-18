using LabWebAPI.Data;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LabWebAPI.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public Item CheckItemIfExists(ItemDto itemCreate)
        {
            return GetItems().Where(i => i.ItemIdentifier.Trim().ToUpper() == itemCreate.ItemIdentifier.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool CreateItem(int softwareId, Item item)
        {
            var software = _context.Softwares.Where(s => s.Id == softwareId).FirstOrDefault();

            var itemSoftware = new ItemSoftware()
            {
                Item = item,
                Software = software,
            };

            _context.Add(itemSoftware);

            //_context.Add(roomId);

            _context.Add(item);

            return Save();
        }

        public bool DeleteItem(Item item)
        {
            _context.Remove(item);
            return Save();
        }

        public Item GetItemById(int itemId)
        {
            return _context.Items.Where(i => i.Id == itemId).FirstOrDefault();
        }

        public ICollection<Item> GetItems()
        {
            return _context.Items
                .Include(i => i.ItemSoftwares)
                .ThenInclude(isr => isr.Software)
                .ToList();
        }

        public bool ItemExists(int itemId)
        {
            return _context.Items.Any(i => i.Id == itemId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateItem(int softwareId, Item item)
        {
            _context.Update(item);
            return Save();
        }
    }
}