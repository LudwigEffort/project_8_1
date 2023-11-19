using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace LabWebAPI.Controllers
{
    [ApiController]
    [Route("LabManager/labUser")]
    public class LabUserController : Controller
    {
        private readonly ILabUserRepository _labUserRepository;
        private readonly IMapper _mapper;

        public LabUserController(ILabUserRepository labUserRepository, IMapper mapper)
        {
            _labUserRepository = labUserRepository;
            _mapper = mapper;
        }

        //* GET Methods
        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LabUser>))]
        public IActionResult GetLabUsers()
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

            //? Lab user get all method
            var users = _mapper.Map<List<LabUserDto>>(_labUserRepository.GetLabUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{labUserId}")]
        [ProducesResponseType(200, Type = typeof(LabUser))]
        [ProducesResponseType(400)]
        public IActionResult GetLabUserById(int labUserId)
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

            //? Lab user get by id method
            if (!_labUserRepository.LabUserExists(labUserId))
            {
                return NotFound();
            }

            var labUser = _mapper.Map<LabUserDto>(_labUserRepository.GetLabUserById(labUserId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(labUser);
        }

        //* POST Method
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateLabUser([FromBody] LabUserDto createLabUser)
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

            //? Lab user post method
            if (createLabUser == null)
            {
                return BadRequest(ModelState);
            }

            //TODO: Move in repository
            //? Checks if new lab user exists by email 
            var labUsers = _labUserRepository.GetLabUsers()
                    .Where(u => u.EmailAddress.Trim().ToUpper() == createLabUser.EmailAddress.TrimEnd().ToUpper())
                    .FirstOrDefault();

            if (labUsers != null)
            {
                ModelState.AddModelError("", "Lab User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var labUserMap = _mapper.Map<LabUser>(createLabUser);

            //TODO: add reservations?

            if (!_labUserRepository.CreateLabUser(labUserMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving lab user data");
                return StatusCode(500, ModelState);
            }

            return Ok(new { labUserMap.Id, Message = "Successfully created" });
        }

        //* PUT Method
        [HttpPut("edit/{labUserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateLabUser(int labUserId, [FromBody] LabUserDto updateLabUser)
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

            //? Lab user put method
            if (updateLabUser == null)
            {
                return BadRequest(ModelState);
            }

            if (labUserId != updateLabUser.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_labUserRepository.LabUserExists(labUserId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var labUserFromDb = _labUserRepository.GetLabUserById(labUserId);

            _mapper.Map(updateLabUser, labUserFromDb);

            if (!_labUserRepository.UpdateLabUser(labUserFromDb))
            {
                ModelState.AddModelError("", "Something went wrong while update lab user data");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //* DELETE Method
        [HttpDelete("delete/{labUserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteLabUser(int labUserId)
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

            //? Lab user delete method
            if (!_labUserRepository.LabUserExists(labUserId))
            {
                return NotFound();
            }

            var labUserToDelete = _labUserRepository.GetLabUserById(labUserId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var test = _labUserRepository.DeleteLabUser(labUserToDelete);

            if (!test)
            {
                ModelState.AddModelError("", "Something went wrong while delete lab user data");
            }

            return NoContent();
        }
    }
}