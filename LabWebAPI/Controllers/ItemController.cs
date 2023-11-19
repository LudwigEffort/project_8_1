using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace LabWebAPI.Controllers
{
    [ApiController]
    [Route("LabManager/item")]
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
        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(Item))]
        public IActionResult GetItems()
        {
            //? Auth
            if (!HttpContext.Items.ContainsKey("User"))
            {
                return Unauthorized("Not authorized!");
            }

            var user = HttpContext.Items["User"] as LabUser;

            if (user == null || (user.Role != "admin" && user.Role != "client"))
            {
                return Unauthorized("Not authorized!");
            }

            //? Item all method
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
            //? Auth
            if (!HttpContext.Items.ContainsKey("User"))
            {
                return Unauthorized("Not authorized!");
            }

            var user = HttpContext.Items["User"] as LabUser;

            if (user == null || user.Role != "admin")
            {
                return Unauthorized("Not authorized!");
            }

            //? Item get by id method
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
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateItem([FromBody] ItemPostDto itemCreate)
        {
            //? Auth
            if (!HttpContext.Items.ContainsKey("User"))
            {
                return Unauthorized("Not authorized!");
            }

            var user = HttpContext.Items["User"] as LabUser;

            if (user == null || user.Role != "admin")
            {
                return Unauthorized("Not authorized!");
            }

            //? Item post method
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

            if (!_itemRepository.CreateItem(itemMap, itemCreate.Softwares))
            {
                ModelState.AddModelError("", "Something wet wrong while saving item data");
                return StatusCode(500, ModelState);
            }

            return Ok(new { itemMap.Id, Message = "Successfully created" });
        }

        //* PUT Method
        [HttpPut("edit/{itemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateItem(int itemId, [FromBody] ItemDto itemUpdate)
        {
            //? Auth
            if (!HttpContext.Items.ContainsKey("User"))
            {
                return Unauthorized("Not authorized!");
            }

            var user = HttpContext.Items["User"] as LabUser;

            if (user == null || user.Role != "admin")
            {
                return Unauthorized("Not authorized!");
            }

            //? Item put method
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

            if (!_itemRepository.UpdateItem(itemFromDb, itemUpdate.Softwares))
            {
                ModelState.AddModelError("", "Some went wrong while updating data");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //* DELETE Method
        [HttpDelete("delete/{itemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteItem(int itemId)
        {
            //? Auth
            if (!HttpContext.Items.ContainsKey("User"))
            {
                return Unauthorized("Not authorized!");
            }

            var user = HttpContext.Items["User"] as LabUser;

            if (user == null || user.Role != "admin")
            {
                return Unauthorized("Not authorized!");
            }

            //? Item delete method
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