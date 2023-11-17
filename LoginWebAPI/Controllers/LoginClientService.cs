using AutoMapper;
using LoginWebAPI.Dto;
using LoginWebAPI.Helper;
using LoginWebAPI.Interfaces;
using LoginWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoginWebAPI.Controllers
{
    [ApiController]
    [Route("auth/client")]
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
        [HttpPost("signUp")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserClientDto createUser)
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
        [HttpPost("signIn")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult LoginClent([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _userClientRepository.GetUserByEmail(loginRequest.Email);

            if (user == null || user.IsBanned == true)
            {
                return Unauthorized("User not found, or is banned");
            }

            var nuance = _authHelper.GenerateNuance(loginRequest.PasswordWithNuance.Length);

            if (!_authHelper.ValidatePasswordWithNuance(loginRequest.PasswordWithNuance, user.Password, nuance))
            {
                return Unauthorized("Email or password is incorrect.");
            }

            string token = _authHelper.GenerateToken(loginRequest.Email);

            return Ok(new { message = "Login successful", Token = token });
        }

    }
}