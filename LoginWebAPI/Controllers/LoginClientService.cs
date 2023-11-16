using AutoMapper;
using LoginWebAPI.Dto;
using LoginWebAPI.Helper;
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
        private readonly AuthHelper _authHelper;
        private readonly IMapper _mapper;

        public LoginClientService(IUserClientRepository userClientRepository, AuthHelper authHelper, IMapper mapper)
        {
            _userClientRepository = userClientRepository;
            _authHelper = authHelper;
            _mapper = mapper;
        }

        //* POST methods
        //? Sign up
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromQuery] UserClientDto createUser)
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

            var nuanceSignup = _authHelper.GenerateNuance(createUser.Password.Length);

            if (!_authHelper.ValidatePasswordWithNuance(createUser.Password, createUser.Password, nuanceSignup))
            {
                return BadRequest("Invalid nuance provided");
            }

            var userMap = _mapper.Map<User>(createUser);

            userMap.Role = "client";
            userMap.CreationTime = DateTime.Now;


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
        public IActionResult LoginClent([FromQuery] string email, [FromQuery] string passwordWithNuance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userClientRepository.GetUserByEmail(email);

            if (user == null || user.IsBanned == true)
            {
                return Unauthorized("User not found, or is banned");
            }

            var nuance = _authHelper.GenerateNuance(passwordWithNuance.Length);

            if (!_authHelper.ValidatePasswordWithNuance(passwordWithNuance, user.Password, nuance))
            {
                return Unauthorized("Email or password is incorrect.");
            }

            string token = _authHelper.GenerateToken(email);

            return Ok(new { message = "Login successful", Token = token });
        }

    }
}