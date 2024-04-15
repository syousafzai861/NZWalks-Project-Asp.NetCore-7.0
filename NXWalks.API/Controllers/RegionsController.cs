using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NXWalks.API.Data;
using NXWalks.API.Models.Domains;
using NXWalks.API.Models.DTO;
using NXWalks.API.Repositories;
using System.Collections.Generic;
using System.Text.Json;

namespace NXWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegionsController : ControllerBase
    {
        private readonly NXWalksDBContext _nXWalksDBContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RegionsController> logger1;

        public RegionsController(NXWalksDBContext nXWalksDBContext, IRegionRepository regionRepository, IMapper mapper,ILogger<RegionsController> logger1)
        {
            this._nXWalksDBContext = nXWalksDBContext;
            this._regionRepository = regionRepository;
            this._mapper = mapper;
            this.logger1 = logger1;
        }


        //Gettting All the regions From the Data base 
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            logger1.LogInformation("Get All Regions Action Methods Was Invoked");

            //Calling Data from the Domain MOdels 
            //var regionsDomain = await _nXWalksDBContext.Regions.ToListAsync();
            var regionsDomain = await _regionRepository.GetAllAsync();

            logger1.LogInformation($"here the get All Method Get Finished With : {JsonSerializer.Serialize(regionsDomain)}");

            // Map the Data to DTOs 
            // var RegionDto = new List<RegionDTO>();
            //  foreach (var region in regionsDomain)
            //  {
            //     RegionDto.Add(new RegionDTO()
            //      {
            //          ID = region.ID,
            //          Name = region.Name,
            //          Code = region.Code,
            //          RegionImgURL = region.RegionImgURL,

            //       });
            //  }

            // Map the Data to DTOs Using Automapping
            var RegionDto = _mapper.Map<List<RegionDTO>>(regionsDomain);

            //Return the Data 
            return Ok(RegionDto);
        }

        //Getting the regions using ID Finding a specific regions
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionsByID([FromRoute] Guid id)
        {
            //First method for finding the Region

            //var regions = _nXWalksDBContext.Regions.Find(id);

            //second method for finding the method region


            // Getting the Data from the Database 
            var regionsDomains = await _regionRepository.GetByID(id);

            if (regionsDomains == null)
            {
                return NotFound();
            }

           // var regionDTO = new RegionDTO()
           // {
            //    ID = regionsDomains.ID,
             //   Name = regionsDomains.Name,
              //  Code = regionsDomains.Code,
               // RegionImgURL = regionsDomains.RegionImgURL,
           // };



            return Ok(_mapper.Map<RegionDTO>(regionsDomains));
        }


        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            if(ModelState.IsValid)
            {
                //Map or convert DTO to domain model 

                //var RegionDomainDto = new Region
                //{
                //    Code = addRegionRequestDto.Code,
                //    Name = addRegionRequestDto.Name,
                //    RegionImgURL = addRegionRequestDto.RegionImgURL,
                //};

                var RegionDomainDto = _mapper.Map<Region>(addRegionRequestDto);

                //use Domain to create that into the data base 
                RegionDomainDto = await _regionRepository.CreateAsync(RegionDomainDto);

                //Map again into DTO to pass the third Parameter which will be return to the CLient 

                //var RegionModelReturn = new RegionDTO
                //{
                //    ID = RegionDomainDto.ID,
                //    Name = RegionDomainDto.Name,
                //    Code = RegionDomainDto.Code,
                //    RegionImgURL = RegionDomainDto.RegionImgURL,
                //};
                var RegionModelReturn = _mapper.Map<RegionDTO>(RegionDomainDto);
                return CreatedAtAction(nameof(GetRegionsByID), new { id = RegionModelReturn.ID }, RegionModelReturn);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async  Task<IActionResult> Update([FromRoute] Guid id , [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map the DTO to RegionDomainModel 
                var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);
                //First Check wheater your record exist in the data base or not 
                regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //now Convert the Domain model to DTO
                var DomainModelDTO = _mapper.Map<RegionDTO>(regionDomainModel);
                return Ok(DomainModelDTO);
            } else
            {
                return BadRequest(ModelState);
            }



        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);
                if(regionDomainModel == null)
            {
                return NotFound();
            }


            //now if you want to return the deleted object you can do it its optional part 

            //var DomainModelDTO = new RegionDTO
            //{
            //    ID = regionDomainModel.ID,
            //    Name = regionDomainModel.Name,
            //    Code = regionDomainModel.Code,
            //    RegionImgURL = regionDomainModel.RegionImgURL
            //};
            var DomainModelDTO = _mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(DomainModelDTO);
        }
  





    }
}
