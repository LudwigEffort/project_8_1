using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace LabWebAPI.Controllers
{
    [ApiController]
    [Route("LabManager/reservation")]
    public class ReservationController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILabUserRepository _labUserRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationController(IItemRepository itemRepository,
            ILabUserRepository labUserRepository,
            IReservationRepository reservationRepository,
            IMapper mapper
        )
        {
            _itemRepository = itemRepository;
            _labUserRepository = labUserRepository;
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        //* GET Methods
        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]
        public IActionResult GetReservations()
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

            //? Reservation get all method
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetReservations());

            //TODO: to remove
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            return Ok(reservations);
        }

        [HttpGet("{reservationId}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        public IActionResult GetRservationById(int reservationId)
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

            //? Reservation get by id method
            if (!_reservationRepository.ReservationExists(reservationId))
            {
                return NotFound();
            }

            var reservation = _mapper.Map<ReservationDto>(_reservationRepository.GetReservationById(reservationId));

            //TODO: to remove
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            return Ok(reservation);
        }

        //* POST Method
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReservation([FromBody] ReservationPostDto createReservation)
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

            //? Reservation post method
            if (createReservation == null)
            {
                return BadRequest(ModelState);
            }

            DateTime startTime = createReservation.StartTime;
            DateTime endTime = startTime.AddHours(2);

            if (!_reservationRepository.IsReservationAvailable(createReservation.ItemId, startTime, endTime))
            {
                ModelState.AddModelError("", "Reservation for that time already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservationMap = _mapper.Map<Reservation>(createReservation);

            reservationMap.StartTime = startTime;
            reservationMap.EndTime = endTime;
            reservationMap.ReservationStatus = "Active";

            if (!_reservationRepository.CreateReservation(reservationMap))
            {
                ModelState.AddModelError("", "Something went wrnog while creating reservation");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        //* DELETE Method
        [HttpDelete("delete/{reservationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReservationByUser(int reservationId)
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

            //? Reservation delete method
            if (!_reservationRepository.ReservationExists(reservationId))
            {
                return NotFound();
            }

            var reservationToDelete = _reservationRepository.GetReservationById(reservationId);

            //TODO: to remove
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            if (!_reservationRepository.DeleteReservation(reservationToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting reservation");
            }

            return NoContent();
        }

    }
}