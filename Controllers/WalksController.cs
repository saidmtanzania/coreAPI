using System.Net;
using AutoMapper;
using coreAPI.Middlewares;
using coreAPI.Models.Domain;
using coreAPI.Models.DTO.Walks;
using coreAPI.Repositories.Walks;
using Microsoft.AspNetCore.Mvc;

namespace coreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository _walksRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WalksController> _logger;

        public WalksController(IWalksRepository walksRepository, IMapper mapper, ILogger<WalksController> logger)
        {
            _walksRepository = walksRepository;
            _mapper = mapper;
            _logger = logger;
        }

        //GET All Walks
        //GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? IsAsceding = true,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000
        )
        {

            //Geting Data from Repository
            List<Walk> walkDomain = await _walksRepository.GetAllAsync(filterOn, filterQuery, sortBy, IsAsceding ?? true, pageNumber, pageSize);
            //Map Domain Model to DTO
            List<WalkDto> walkDto = _mapper.Map<List<WalkDto>>(walkDomain);
            //Return Walks to Client
            return Ok(walkDto);

        }

        //GET Walk By Id
        //GET
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            //Geting Data from Repository
            Walk? walkDomain = await _walksRepository.GetByIdAsync(id);
            //Checking if return data is not null
            if (walkDomain == null)
            {
                return NotFound();
            }
            //Map Domain Model to DTO
            WalkDto walkDto = _mapper.Map<WalkDto>(walkDomain);
            //Return Walk to Client
            return Ok(walkDto);
        }

        //CREATE Walk
        //POST
        [HttpPost]
        [ModelValidation]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            //Map DTO to Domain Model
            Walk walkDomain = _mapper.Map<Walk>(addWalksRequestDto);
            //Use Domain Model to create walk
            walkDomain = await _walksRepository.CreateAsync(walkDomain);
            //Map Domain Model to DTO
            WalkDto walkDto = _mapper.Map<WalkDto>(walkDomain);
            //Return Created walk to Client
            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);
        }

        //UPDATE Walk
        //PUT
        [HttpPut("{id:Guid}")]
        [ModelValidation]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksRequestDto updateWalksRequestDto)
        {
            //Map DTO to Domain Model
            Walk? walkDomain = _mapper.Map<Walk>(updateWalksRequestDto);
            //Use Domain Model to update walk
            walkDomain = await _walksRepository.UpdateAsync(id, walkDomain);
            //Check if walk doesnt exist and return response
            if (walkDomain == null)
            {
                return NotFound();

            }
            //Map Domain Model to DTO
            WalkDto walkDto = _mapper.Map<WalkDto>(walkDomain);
            //Return Updated walk to Client
            return Ok(walkDto);
        }

        //DELETE Walk
        //DELETE
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            //fetching a existing walk from Repository and delete it
            Walk? walkDomain = await _walksRepository.DeleteAsync(id);
            //Check if region doesnt exist and return response
            if (walkDomain == null)
            {
                return NotFound();
            }
            //Return Deleted walk to Client
            return Ok(walkDomain); //Return Deleted walk to Client
        }
    }
}