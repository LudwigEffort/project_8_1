using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LabWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Room>))]
        public IActionResult GetRooms()
        {

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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRoom([FromBody] RoomDto createRoom)
        {
            if (createRoom == null)
            {
                return BadRequest(ModelState);
            }

            var room = _roomRepository.GetRooms()
                .Where(r => r.RoomName.Trim().ToUpper() == createRoom.RoomName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomMap = _mapper.Map<Room>(createRoom);

            if (!_roomRepository.CreateRoom(roomMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving room data");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        //* PUT Method
        [HttpPut("{roomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRoom(int roomId, [FromBody] RoomDto updateRoom)
        {
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
            }

            return NoContent();
        }

        //* DELETE Method
        [HttpDelete("{roomId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRoom(int roomId)
        {
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