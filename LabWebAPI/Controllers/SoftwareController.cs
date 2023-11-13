using AutoMapper;
using LabWebAPI.Dto;
using LabWebAPI.Interfaces;
using LabWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace LabWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Software>))]
        public IActionResult GetSoftwares()
        {

            var softwares = _mapper.Map<List<SoftwareDto>>(_softwareRepository.GetSoftwares());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(softwares);
        }

        [HttpGet("{softwareId}")]
        [ProducesResponseType(200, Type = typeof(Software))]
        [ProducesResponseType(400)]
        public IActionResult GetSoftwareById(int softwareId)
        {

            if (!_softwareRepository.SoftwareExists(softwareId))
            {
                return NotFound();
            }

            var software = _mapper.Map<SoftwareDto>(_softwareRepository.GetSoftwareById(softwareId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(software);
        }

        //* POST method
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSoftware([FromBody] SoftwareDto createSoftware)
        {
            if (createSoftware == null)
            {
                return BadRequest(ModelState);
            }

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

            return Ok("Successfully created");
        }

        //* PUT method
        [HttpPut("{softwareId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSfotware(int softwareId, [FromBody] SoftwareDto updateSoftware)
        {
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
        [HttpDelete("{softwareId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSoftware(int softwareId)
        {
            if (!_softwareRepository.SoftwareExists(softwareId))
            {
                return NotFound();
            }

            var softwareToDelete = _softwareRepository.GetSoftwareById(softwareId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_softwareRepository.DeleteSoftware(softwareToDelete))
            {
                ModelState.AddModelError("", "Something went wrong while delete Software data");
            }

            return NoContent();
        }

    }
}