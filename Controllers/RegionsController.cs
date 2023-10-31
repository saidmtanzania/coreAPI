using AutoMapper;
using coreAPI.Data;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO;
using coreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace coreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly CoreDbContext _coreDbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(CoreDbContext coreDbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            _coreDbContext = coreDbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        //GET all regions
        //GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Repository
            var regionsDomain = await _regionRepository.GetAllAsync();

            //Map Domain Model to DTOs
            var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);

            //Return DTOs to client
            return Ok(regionsDto);
        }

        //GET one region
        //GET(id)
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Data From Repository
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain is null)
            {
                return NotFound();
            }

            //Map Domain Model to DTOs
            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            //Return DTOs to client
            return Ok(regionDto);
        }

        //ADD region
        //POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

            //Use Domain Model to create Region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            //Map Domain Model to DTOs
            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //UPDATE region
        //PUT
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateDto)
        {
            //Map DTO to Domain Model
            var regionDomain = _mapper.Map<Region>(updateDto);

            //Query and Check if region exist
            regionDomain = await _regionRepository.UpdateAsync(id, regionDomain);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTOs
            var regionDto = _mapper.Map<UpdateRegionDto>(regionDomain);

            //return updated region
            return Ok(regionDto);
        }

        //DELETE region
        //DELETE
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //find by other entity
            var regionDomain = await _regionRepository.DeleteAsync(id);
            if (regionDomain is null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTOs
            var regionDto = _mapper.Map<RegionDto>(regionDomain);

            // return NoContent();
            return Ok(regionDto);
        }
    }
}


