using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NXWalks.API.Models.Domains;
using NXWalks.API.Models.DTO;
using NXWalks.API.Repositories;
using System.Net;

namespace NXWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalksRepositories walksRepositories;
        private readonly ILogger<WalksController> logger1;

        //Walks Controller

        public WalksController(IMapper mapper, IWalksRepositories walksRepositories,ILogger<WalksController> logger1)
        {
            this.mapper = mapper;
            this.walksRepositories = walksRepositories;
            this.logger1 = logger1;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            if (ModelState.IsValid)
            {
                //mapping the coming data to DTO 
                var WalksDTO = mapper.Map<Walks>(addWalkRequestDTO);

                await walksRepositories.CreateAsync(WalksDTO);

                //Again map it to a DTO 
                var Walks = mapper.Map<WalksDTO>(WalksDTO);
                return Ok(Walks);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn , [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy , [FromQuery] bool? isAssending,
            [FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 10)
        {
            try
            {
                var walkDomainModel = await walksRepositories.GetAllWalksAsync(filterOn, filterQuery, sortBy, isAssending ?? true, pageNumber, pageSize);

                return Ok(mapper.Map<List<WalksDTO>>(walkDomainModel));
            }
            catch (Exception ex)
            {
                logger1.LogInformation(ex, "Exception Logs For Get All Walks Api");
                return Problem("Something Wnet Wrong", null, (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkID([FromRoute] Guid id)
        {
            var getWalk = await walksRepositories.GetWalkByID(id);

            if (getWalk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalksDTO>(getWalk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatedWalks([FromRoute] Guid id , UpdateRequestWalkDTO updateRequestWalkDTO)
        {
            if(ModelState.IsValid)
            {
                var updateDomainModel = mapper.Map<Walks>(updateRequestWalkDTO);

                updateDomainModel = await walksRepositories.UpdateWalkAsync(id, updateDomainModel);

                if (updateDomainModel == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<WalksDTO>(updateDomainModel));

            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalksMethod([FromRoute] Guid id)
        {
            var deletedmodel = await walksRepositories.DeleteWalk(id);
            if(deletedmodel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalksDTO>(deletedmodel));
        }


    }
}
