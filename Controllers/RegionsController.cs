using coreAPI.Data;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            //Get Data From Database -Domain Model
            var regionsDomain = this.coreDbContext.Regions.ToList();
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
        public IActionResult GetById([FromRoute] Guid id)
        {
            //find by only ID
            // var regionDomain = this.coreDbContext.Regions.Find(id);

            //find by other entity
            var regionDomain = this.coreDbContext.Regions.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Create([FromBody] AddRegionRequestDto requestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = requestDto.Code,
                Name = requestDto.Name,
                RegionImageUrl = requestDto.RegionImageUrl
            };

            //Use Domain Model to create Region
            this.coreDbContext.Regions.Add(regionDomainModel);
            this.coreDbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateDto)
        {
            //find by only ID
            // var regionDomain = this.coreDbContext.Regions.Find(id);

            //find by other entity
            var regionDomain = this.coreDbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomain is null)
            {
                return NotFound();
            }

            //Update Region
            regionDomain.Code = updateDto.Code;
            regionDomain.Name = updateDto.Name;
            regionDomain.RegionImageUrl = updateDto.RegionImageUrl;

            this.coreDbContext.SaveChanges();

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
    }
}


