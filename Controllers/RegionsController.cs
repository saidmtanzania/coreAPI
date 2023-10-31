using coreAPI.Data;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace coreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly CoreDbContext coreDbContext;

        public RegionsController(CoreDbContext coreDbContext)
        {
            this.coreDbContext = coreDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database -Domain Model
            var regionsDomain = await this.coreDbContext.Regions.ToListAsync();
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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //find by only ID
            // var regionDomain = this.coreDbContext.Regions.Find(id);

            //find by other entity
            var regionDomain = await this.coreDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

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
            await this.coreDbContext.Regions.AddAsync(regionDomainModel);
            await this.coreDbContext.SaveChangesAsync();

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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateDto)
        {
            //find by only ID
            // var regionDomain = this.coreDbContext.Regions.Find(id);

            //find by other entity
            var regionDomain = await this.coreDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain is null)
            {
                return NotFound();
            }

            //Update Region
            regionDomain.Code = updateDto.Code;
            regionDomain.Name = updateDto.Name;
            regionDomain.RegionImageUrl = updateDto.RegionImageUrl;

            await this.coreDbContext.SaveChangesAsync();

            //Convert Domain Model to DTOs
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
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
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //find by only ID
            // var regionDomain = this.coreDbContext.Regions.Find(id);

            //find by other entity
            var regionDomain = await this.coreDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomain is null)
            {
                return NotFound();
            }

            //Delete Region
            this.coreDbContext.Regions.Remove(regionDomain);
            await this.coreDbContext.SaveChangesAsync();

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


