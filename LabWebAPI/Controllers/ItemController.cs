using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace LabWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILabUserRepository _labUserRepository;
        private readonly ISoftwareRepository _softwareRepository;
        private readonly IMapper _mapper;

        public ItemController(IItemRepository itemRepository, ILabUserRepository labUserRepository,
        ISoftwareRepository softwareRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _labUserRepository = labUserRepository;
            _softwareRepository = softwareRepository;
            _mapper = mapper;
        }

        //* GET Methods
        //TODO: add auth for user client
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Item))]
        public IActionResult GetItems()
        {
            var user = HttpContext.Items["User"] as LabUser;

            if (user == null || user.Role != "admin")
            {
                return Unauthorized("Not authorized!");
            }

            var items = _mapper.Map<List<ItemDto>>(_itemRepository.GetItems());

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(items);
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(200, Type = typeof(Item))]
        [ProducesResponseType(400)]
        public IActionResult GetItemById(int itemId)
        {

            if (!_itemRepository.ItemExists(itemId))
            {
                return NotFound();
            }

            var item = _mapper.Map<ItemDto>(_itemRepository.GetItemById(itemId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(item);
        }

        //* POST Method
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateItem([FromQuery] int softwareId, [FromBody] ItemDto itemCreate)
        {
            if (itemCreate == null)
            {
                return BadRequest(ModelState);
            }

            var items = _itemRepository.CheckItemIfExists(itemCreate);

            if (items != null)
            {
                ModelState.AddModelError("", "Item already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var itemMap = _mapper.Map<Item>(itemCreate);

            if (!_itemRepository.CreateItem(softwareId, itemMap))
            {
                ModelState.AddModelError("", "Something wet wrong while saving item data");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        //* PUT Method
        [HttpPut("{itemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateItem(int itemId, [FromQuery] int softwareId, [FromBody] ItemDto itemUpdate)
        {
            if (itemUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (itemId != itemUpdate.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_itemRepository.ItemExists(itemId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var itemFromDb = _itemRepository.GetItemById(itemId);

            _mapper.Map(itemUpdate, itemFromDb);

            if (!_itemRepository.UpdateItem(softwareId, itemFromDb))
            {
                ModelState.AddModelError("", "Some went wrong while updating data");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //* DELETE Method
        [HttpDelete("{itemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteItem(int itemId)
        {
            if (!_itemRepository.ItemExists(itemId))
            {
                return NotFound();
            }

            var itemToDelete = _itemRepository.GetItemById(itemId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_itemRepository.DeleteItem(itemToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting item");
            }

            return NoContent();
        }
    }
}