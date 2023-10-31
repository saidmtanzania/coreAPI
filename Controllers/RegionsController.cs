using coreAPI.Data;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO;
using coreAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly CoreDbContext _coreDbContext;

        private readonly IRegionRepository _regionRepository;

        public RegionsController(CoreDbContext coreDbContext, IRegionRepository regionRepository)
        {
            _coreDbContext = coreDbContext;
            _regionRepository = regionRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database -Domain Model
            var regionsDomain = await _regionRepository.GetAllAsync();
            if (regionsDomain == null || regionsDomain.Count == 0)
            {
                return NotFound();
            }

            //Map Domain Model to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            //Return DTOs to client
            return Ok(regionsDto);
        }

        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //find by only ID
            // var regionDomain = this._coreDbContext.Regions.Find(id);

            //find by other entity
            var regionDomain = await _regionRepository.GetByIdAsync(id);

            if (regionDomain is null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTOs
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto requestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = requestDto.Code,
                Name = requestDto.Name,
                RegionImageUrl = requestDto.RegionImageUrl
            };

            //Use Domain Model to create Region
            regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

            //Map Domain Model to DTOs
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Update region
        //PUT
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateDto)
        {
            //Map DTO to Domain Model
            var regionDomain = new Region
            {
                Code = updateDto.Code,
                Name = updateDto.Name,
                RegionImageUrl = updateDto.RegionImageUrl,
            };

            //Query and Check if region exist
            regionDomain = await _regionRepository.UpdateAsync(id, regionDomain);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Convert Domain Model to DTOs
            var regionDto = new RegionDto
            {
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

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
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };

            // return NoContent();
            return Ok(regionDto);
        }
    }
}


