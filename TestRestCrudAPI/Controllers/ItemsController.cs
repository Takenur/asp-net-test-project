using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Route("items")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepo _repository;
        private readonly IMapper _mapper;

        public ItemsController(IItemsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //private readonly MockItemRepo _repository = new MockItemRepo();

        [HttpGet]
        
        public ActionResult<IEnumerable<ItemReadDto>> GetAllItems([FromQuery] PaginationQuery paginationQuery) {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var items = _repository.GetItems(paginationFilter);

            int count = _repository.GetCount();

            var ordersResponse = _mapper.Map<List<ItemReadDto>>(items);

            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<ItemReadDto>(ordersResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginationResponse<ItemReadDto>(ordersResponse, paginationFilter,count);

            return Ok(paginationResponse);
        }
        [HttpGet("{id}",Name = "GetItemById")]
        public ActionResult<ItemReadDto> GetItemById(int id){
            var item = _repository.GetItemById(id);

            return Ok(_mapper.Map<ItemReadDto>(item));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult<ItemReadDto> CreateItems( ItemsCreateDto itemsCreateDto) {

            var ItemModel = _mapper.Map<Items>(itemsCreateDto);
            _repository.CreateItems(ItemModel);
            _repository.SaveChanges();

            var itemReadDto = _mapper.Map<ItemReadDto>(ItemModel);

            return CreatedAtRoute(nameof(GetItemById),new { id=itemReadDto.id},itemReadDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult UpdateItem(int id, ItemUpdateDto itemUpdate) {
            var itemModelFromRepo = _repository.GetItemById(id);
            if (itemModelFromRepo == null) {
                return NotFound();
            }
            _mapper.Map(itemUpdate, itemModelFromRepo);
            _repository.UpdateItems(itemModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }
        [HttpPatch("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult PartialItemUpdate(int id, JsonPatchDocument<ItemUpdateDto> patchItem)
        {
            var itemModelFromRepo = _repository.GetItemById(id);
            if (itemModelFromRepo == null)
            {
                return NotFound();
            }

            var itemToPatch = _mapper.Map<ItemUpdateDto>(itemModelFromRepo);
            patchItem.ApplyTo(itemToPatch,ModelState);
            if (!TryValidateModel(itemToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(itemToPatch, itemModelFromRepo);

            _repository.UpdateItems(itemModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteItem(int id)
        {
            var itemModelFromRepo = _repository.GetItemById(id);
            if (itemModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteItem(itemModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

    }
}
