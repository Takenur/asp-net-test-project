using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestRestCrudAPI.Data;
using TestRestCrudAPI.Domain;
using TestRestCrudAPI.Dtos;
using TestRestCrudAPI.Helpers;
using TestRestCrudAPI.Models;
using TestRestCrudAPI.Requests.Queries;
using TestRestCrudAPI.Response;

namespace TestRestCrudAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionsRepo _repository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Regions>> GetAllRegions([FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var items = _repository.GetRegions(paginationFilter);
            var regionsResponse = _mapper.Map<List<RegionsReadDto>>(items);

            int count = _repository.GetCount();
            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<RegionsReadDto>(regionsResponse));
            }
            var paginationResponse = PaginationHelpers.CreatePaginationResponse<RegionsReadDto>(regionsResponse, paginationFilter, count);
            return Ok(paginationResponse);


        }
        [HttpGet("{id}")]
        public ActionResult<Regions> GetRegionById([FromQuery] PaginationQuery paginationQuery, int id)
        {
            
            var item = _repository.GetRegionById(id);
            var regionsResponse = _mapper.Map<List<RegionsReadDto>>(item);
            
           
            return Ok(regionsResponse);
        }
        
        // create (Post)
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<RegionsReadDto> CreateRegion(RegionsCreateDto regionsCreateDto)
        {

            var RegionModel = _mapper.Map<Regions>(regionsCreateDto);
            _repository.CreateRegions(RegionModel);
            _repository.SaveChanges();

            var regionReadDto = _mapper.Map<RegionsReadDto>(RegionModel);

            return CreatedAtRoute(nameof(GetRegionById), new { id = regionReadDto.Id }, regionReadDto);
        }

        // update (Put)

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateRegion(int id, RegionsReadDto regionUpdate)
        {
            var regionModelFromRepo = _repository.GetRegionById(id);
            if (regionModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(regionUpdate, regionModelFromRepo);
            _repository.UpdateRegions(regionModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        // update (Patch)
        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult PartialRegionUpdate(int id, JsonPatchDocument<RegionsUpdateDto> patchRegion)
        {
            var regionModelFromRepo = _repository.GetRegionById(id);
            if (regionModelFromRepo == null)
            {
                return NotFound();
            }

            var regionToPatch = _mapper.Map<RegionsUpdateDto>(regionModelFromRepo);
            patchRegion.ApplyTo(regionToPatch, ModelState);
            if (!TryValidateModel(regionToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(regionToPatch, regionModelFromRepo);

            _repository.UpdateRegions(regionModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteRegion(int id)
        {
            var regionModelFromRepo = _repository.GetRegionById(id);
            if (regionModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteRegion(regionModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
