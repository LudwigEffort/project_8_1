using AutoMapper;
using LoginWebAPI.Dto;
using LoginWebAPI.Interfaces;
using LoginWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginClientService : Controller
    {
        private readonly IUserClientRepository _userClientRepository;
        private readonly IMapper _mapper;

        public LoginClientService(IUserClientRepository userClientRepository, IMapper mapper)
        {
            _userClientRepository = userClientRepository;
            _mapper = mapper;
        }

        //* POST methods
        //? Sign up
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromForm] UserClientDto createUser)
        {
            if (createUser == null)
            {
                return BadRequest(ModelState);
            }

            var user = _userClientRepository.GetUsers()
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

            if (!_userClientRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving data");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        //? Login 
        [HttpPost("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult LoginClent(string email, string password)
        {

            var user = _userClientRepository.GetUserByEmail(email);

            if (user == null || user.Password != password)
            {
                return Unauthorized("Email or password is incorrect.");
            }

            //TODO: add token for lab manager API
            //TODO: insert ModelState.IsValid?

            return Ok("Login successful");
        }

    }
}