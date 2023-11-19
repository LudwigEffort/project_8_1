using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace LabWebAPI.Controllers
{
    [ApiController]
    [Route("LabManager/room")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomController(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        //* GET Methods
        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Room>))]
        public IActionResult GetRooms()
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

            //? Room get method         
            var rooms = _mapper.Map<List<RoomDto>>(_roomRepository.GetRooms());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(rooms);
        }

        [HttpGet("{roomId}")]
        [ProducesResponseType(200, Type = typeof(Room))]
        [ProducesResponseType(400)]
        public IActionResult GetSoftwareById(int roomId)
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

            //? Room get by id method
            if (!_roomRepository.RoomExsits(roomId))
            {
                return NotFound();
            }

            var room = _mapper.Map<RoomDto>(_roomRepository.GetRoomById(roomId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(room);
        }

        //* POST Method
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRoom([FromBody] RoomDto createRoom)
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

            //? Room post method
            if (createRoom == null)
            {
                return BadRequest(ModelState);
            }

            //? Checks if new room exists by name 
            var rooms = _roomRepository.GetRooms()
                .Where(r => r.RoomName.Trim().ToUpper() == createRoom.RoomName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (rooms != null)
            {
                ModelState.AddModelError("", "Room already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomMap = _mapper.Map<Room>(createRoom);

            //TODO: add items and pc?

            if (!_roomRepository.CreateRoom(roomMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving room data");
                return StatusCode(500, ModelState);
            }

            return Ok(new { roomMap.Id, Message = "Successfully created" });
        }

        //* PUT Method
        [HttpPut("edit/{roomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRoom(int roomId, [FromBody] RoomDto updateRoom)
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

            //? Room put method
            if (updateRoom == null)
            {
                return BadRequest(ModelState);
            }

            if (roomId != updateRoom.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_roomRepository.RoomExsits(roomId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomFromDb = _roomRepository.GetRoomById(roomId);

            _mapper.Map(updateRoom, roomFromDb);

            if (!_roomRepository.UpdateRoom(roomFromDb))
            {
                ModelState.AddModelError("", "Something went wrong while update room data");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //* DELETE Method
        [HttpDelete("delete/{roomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRoom(int roomId)
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

            //? Room delete method
            if (!_roomRepository.RoomExsits(roomId))
            {
                return NotFound();
            }

            var roomToDelete = _roomRepository.GetRoomById(roomId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_roomRepository.DeleteRoom(roomToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while delete room data");
            }

            return NoContent();
        }
    }
}