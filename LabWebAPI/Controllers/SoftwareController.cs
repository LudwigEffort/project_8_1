using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace LabWebAPI.Controllers
{
    [ApiController]
    [Route("LabManager/software")]
    public class SoftwareController : Controller
    {
        private readonly ISoftwareRepository _softwareRepository;
        private readonly IMapper _mapper;

        public SoftwareController(ISoftwareRepository softwareRepository, IMapper mapper)
        {
            _softwareRepository = softwareRepository;
            _mapper = mapper;
        }

        //* GET methods
        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Software>))]
        public IActionResult GetSoftwares()
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

            //? Software get method
            var softwares = _mapper.Map<List<SoftwareDto>>(_softwareRepository.GetSoftwares());

            //TODO: to remove
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            return Ok(softwares);
        }

        [HttpGet("{softwareId}")]
        [ProducesResponseType(200, Type = typeof(Software))]
        [ProducesResponseType(400)]
        public IActionResult GetSoftwareById(int softwareId)
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

            //? Software get by id method
            if (!_softwareRepository.SoftwareExists(softwareId))
            {
                return NotFound();
            }

            var software = _mapper.Map<SoftwareDto>(_softwareRepository.GetSoftwareById(softwareId));

            //TODO: to remove
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            return Ok(software);
        }

        //* POST method
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSoftware([FromBody] SoftwareDto createSoftware)
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

            //? Software post method
            if (createSoftware == null)
            {
                return BadRequest(ModelState);
            }

            //? Checks if new software exists by name 
            var software = _softwareRepository.GetSoftwares()
                .Where(s => s.SoftwareName.Trim().ToUpper() == createSoftware.SoftwareName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (software != null)
            {
                ModelState.AddModelError("", "Software already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var softwareMap = _mapper.Map<Software>(createSoftware);

            if (!_softwareRepository.CreateSoftware(softwareMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving software data");
                return StatusCode(500, ModelState);
            }

            return Ok(new { softwareMap.Id, Message = "Successfully created" });
        }

        //* PUT method
        [HttpPut("edit/{softwareId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSfotware(int softwareId, [FromBody] SoftwareDto updateSoftware)
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

            //? Software put method
            if (updateSoftware == null)
            {
                return BadRequest(ModelState);
            }

            if (softwareId != updateSoftware.Id)
            {
                return BadRequest(ModelState);
            }

            if (!_softwareRepository.SoftwareExists(softwareId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var softwareFromDb = _softwareRepository.GetSoftwareById(softwareId);

            _mapper.Map(updateSoftware, softwareFromDb);

            if (!_softwareRepository.UpdateSoftware(softwareFromDb))
            {
                ModelState.AddModelError("", "Something went wrong while update Software data");
            }

            return NoContent();
        }

        //* DELETE method
        [HttpDelete("delete/{softwareId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSoftware(int softwareId)
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

            //? Software delete method
            if (!_softwareRepository.SoftwareExists(softwareId))
            {
                return NotFound();
            }

            var softwareToDelete = _softwareRepository.GetSoftwareById(softwareId);

            //TODO: to remove
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState);
            // }

            if (!_softwareRepository.DeleteSoftware(softwareToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while delete Software data");
            }

            return NoContent();
        }

    }
}