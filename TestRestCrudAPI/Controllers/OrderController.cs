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
    public class OrderController : ControllerBase
    {
        private readonly IOrdersRepo _repository;
        private readonly IMapper _mapper;

        public OrderController(IOrdersRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Orders>> GetAllOrders([FromQuery] PaginationQuery paginationQuery)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);

            var items = _repository.getOrders(paginationFilter);

            var ordersResponse = _mapper.Map<List<OrdersReadDto>>(items);

            int count = _repository.GetCount();


            if (paginationFilter==null || paginationFilter.PageNumber<1 || paginationFilter.PageSize<1) {
                return Ok(new PagedResponse<OrdersReadDto>(ordersResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginationResponse<OrdersReadDto>(ordersResponse, paginationFilter, count);

            return Ok(paginationResponse);
        }
        [HttpGet("{id}")]
        public ActionResult<Orders> GetOrderById(int id)
        {
            var item = _repository.GetOrderById(id);

            return Ok(item);
        }
        [HttpGet("region/{region}")]
        public ActionResult<Orders> GetOrdersByRegion([FromQuery] PaginationQuery paginationQuery, string region) {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var item = _repository.getOrdersByRegion(region, paginationFilter);
            if (item==null) { 
            return NotFound();
            }
            int count = _repository.GetCountByRegion(region);
            var ordersResponse = _mapper.Map<List<OrdersReadDto>>(item);
            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<OrdersReadDto>(ordersResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginationResponse<OrdersReadDto>(ordersResponse, paginationFilter, count);
            return Ok(paginationResponse);
        }

        [HttpGet("items/{item}")]
        public ActionResult<Orders> GetOrdersByItem([FromQuery] PaginationQuery paginationQuery, string item)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var rep = _repository.getOrdersByItems(item);
            int count = _repository.GetCountByItems(item);
            if (rep == null)
            {
                return NotFound();
            }
            var ordersResponse = _mapper.Map<List<OrdersReadDto>>(rep);
            if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
            {
                return Ok(new PagedResponse<OrdersReadDto>(ordersResponse));
            }

            var paginationResponse = PaginationHelpers.CreatePaginationResponse<OrdersReadDto>(ordersResponse, paginationFilter,count);

            return Ok(paginationResponse);
        }

        [HttpPost]
        public ActionResult<OrdersReadDto> CreateOrder(OrdersCreateDto itemsCreateDto)
        {

            var OrderModel = _mapper.Map<Orders>(itemsCreateDto);
            _repository.CreateOrders(OrderModel);
            _repository.SaveChanges();

            var orderReadDto = _mapper.Map<OrdersReadDto>(OrderModel);

            return CreatedAtRoute(nameof(GetOrderById), new { id = orderReadDto.id }, orderReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, OrdersReadDto orderUpdate)
        {
            var orderModelFromRepo = _repository.GetOrderById(id);
            if (orderModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(orderUpdate, orderModelFromRepo);
            _repository.UpdateOrders(orderModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialItemUpdate(int id, JsonPatchDocument<OrdersUpdateDto> patchItem)
        {
            var orderModelFromRepo = _repository.GetOrderById(id);
            if (orderModelFromRepo == null)
            {
                return NotFound();
            }

            var orderToPatch = _mapper.Map<OrdersUpdateDto>(orderModelFromRepo);
            patchItem.ApplyTo(orderToPatch, ModelState);
            if (!TryValidateModel(orderToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(orderToPatch, orderModelFromRepo);

            _repository.UpdateOrders(orderModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteOrder(int id)
        {
            var orderModelFromRepo = _repository.GetOrderById(id);
            if (orderModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteOrder(orderModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
