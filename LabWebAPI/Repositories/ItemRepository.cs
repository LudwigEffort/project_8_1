using LabWebAPI.Controllers;
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

        public Item CheckItemIfExists(ItemPostDto itemCreate)
        {
            return GetItems().Where(i => i.ItemIdentifier.Trim().ToUpper() == itemCreate.ItemIdentifier.TrimEnd().ToUpper())
                .FirstOrDefault();

        }

        public bool CreateItem(Item item, List<SoftwareDto> softwares)
        {
            _context.Add(item);
            bool saved = Save();

            if (!saved) return false;

            foreach (var softtwareDto in softwares)
            {
                var software = _context.Softwares.FirstOrDefault(s => s.Id == softtwareDto.Id);
                if (software != null)
                {
                    _context.ItemSoftwares.Add(new ItemSoftware { ItemId = item.Id, SoftwareId = software.Id });
                }
            }

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

        public bool UpdateItem(Item item, List<SoftwareDto> softwares)
        {
            var existingAssociations = _context.ItemSoftwares.Where(a => a.ItemId == item.Id);
            _context.ItemSoftwares.RemoveRange(existingAssociations);

            foreach (var softtwareDto in softwares)
            {
                var software = _context.Softwares.FirstOrDefault(s => s.Id == softtwareDto.Id);
                if (software != null)
                {
                    _context.ItemSoftwares.Add(new ItemSoftware { ItemId = item.Id, SoftwareId = software.Id });
                }
            }

            _context.Update(item);
            return Save();
        }
    }
}