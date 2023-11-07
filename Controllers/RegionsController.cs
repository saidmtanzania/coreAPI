using System.Text.Json;
using AutoMapper;
using coreAPI.Middlewares;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO.Regions;
using coreAPI.Repositories.Regions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace coreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //GET all regions
        //GET
        [HttpGet]
        // [Authorize(Roles = "Reader")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                throw new Exception("Pull Over here x");
                //Get Data From Repository
                List<Region> regionDomain = await _regionRepository.GetAllAsync();
                //Map Domain Model to DTOs
                List<RegionDto> regionDto = _mapper.Map<List<RegionDto>>(regionDomain);
                //Return DTOs to client
                return Ok(regionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
                return BadRequest(ex.Message);
            }
        }

        //GET one region
        //GET(id)
        [HttpGet("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Data From Repository
            Region? regionDomain = await _regionRepository.GetByIdAsync(id);
            //Check if region exist
            if (regionDomain is null)
            {
                return NotFound();
            }
            //Map Domain Model to DTOs
            RegionDto regionDto = _mapper.Map<RegionDto>(regionDomain);
            //Return DTOs to client
            return Ok(regionDto);
        }

        //ADD region
        //POST
        [HttpPost]
        [ModelValidation]
        [Authorize(Roles = "Writer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            //Map or Convert DTO to Domain Model
            Region regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);
            //Use Domain Model to create Region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);
            //Map Domain Model to DTOs
            RegionDto regionDto = _mapper.Map<RegionDto>(regionDomainModel);
            //return created region response
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        //UPDATE region
        //PUT
        [HttpPut("{id:Guid}")]
        [ModelValidation]
        [Authorize(Roles = "Writer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateDto)
        {

            //Map DTO to Domain Model
            Region? regionDomain = _mapper.Map<Region>(updateDto);
            //Query and Check if region exist
            regionDomain = await _regionRepository.UpdateAsync(id, regionDomain);
            //Check if region exist and return response
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Convert Domain Model to DTOs
            UpdateRegionDto regionDto = _mapper.Map<UpdateRegionDto>(regionDomain);
            //return updated region
            return Ok(regionDto);

        }

        //DELETE region
        //DELETE
        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //find region if available and delete it.
            Region? regionDomain = await _regionRepository.DeleteAsync(id);
            //Check if region exist
            if (regionDomain is null)
            {
                return NotFound();
            }
            //Convert Domain Model to DTOs
            RegionDto regionDto = _mapper.Map<RegionDto>(regionDomain);
            // return deleted region
            return Ok(regionDto);
        }
    }
}


