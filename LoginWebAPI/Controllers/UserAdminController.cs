using AutoMapper;
using LoginWebAPI.Dto;
using LoginWebAPI.Interfaces;
using LoginWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAdminController : Controller
    {
        private readonly IUserAdminRepository _userAdminRepository;
        private readonly IMapper _mapper;

        public UserAdminController(IUserAdminRepository userAdminRepository, IMapper mapper)
        {
            _userAdminRepository = userAdminRepository;
            _mapper = mapper;
        }

        //* GET methods
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserAdminDto>>(_userAdminRepository.GetUsers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_userAdminRepository.UserExists(userId))
            {
                return NotFound();
            }

            var user = _mapper.Map<UserAdminDto>(_userAdminRepository.GetUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        //* POST method
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserAdminPostDto createUser)
        {
            if (createUser == null)
            {
                return BadRequest(ModelState);
            }

            var user = _userAdminRepository.GetUsers()
                .Where(u => u.EmailAddress.Trim().ToUpper() == createUser.EmailAddress.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userMap = _mapper.Map<User>(createUser);

            if (!_userAdminRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving data");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        //* PUT method
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserAdminDto updateUser)
        {
            if (updateUser == null)
            {
                return BadRequest(ModelState);
            }

            if (userId != updateUser.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_userAdminRepository.UserExists(userId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userFromDb = _userAdminRepository.GetUser(userId);

            _mapper.Map(updateUser, userFromDb);

            if (!_userAdminRepository.UpdateUser(userFromDb))
            {
                ModelState.AddModelError("", "Something went wrong while saving data");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        //* DELTE method
        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userAdminRepository.UserExists(userId))
            {
                return NotFound();
            }

            var userToDelete = _userAdminRepository.GetUser(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_userAdminRepository.DeliteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while deleting user");
            }

            return NoContent();
        }

    }
}