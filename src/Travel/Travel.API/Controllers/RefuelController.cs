using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Travel.API.Infrastructure.Queries;
using Travel.API.Infrastructure.Services;
using Travel.API.ViewModel;
using Travel.Domain.AggregatesModel.RefuelAggregate;

namespace Travel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefuelController : ControllerBase
    {
        private readonly IRefuelService _refuelService;
        private readonly IRefuelQueries _refuelQueries;
        private readonly IMapperService _mapper;

        public RefuelController(IRefuelService refuelService, IRefuelQueries refuelQueries, IMapperService mapper)
        {
            _refuelService = refuelService;
            _refuelQueries = refuelQueries;
            _mapper = mapper;
        }

        [Route("items/{ids?}")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResults<RefuelItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<RefuelViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0, string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var arrayItems = await GetItemsByIdsAsync(ids);

                if (!arrayItems.Any())
                {
                    return BadRequest("ids value invalid. Must be comma-separated list of numbers");
                }

                return Ok(arrayItems);
            }

            var totalItems = await _refuelQueries.RefuelCountAsync();

            var items = await _refuelQueries.GetPagedAsync(pageSize, pageIndex);
            
            var results = new PaginatedResults<RefuelViewModel>(pageIndex, pageSize, totalItems, items);

            return Ok(results);
        }

        private async Task<List<RefuelViewModel>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<RefuelViewModel>();
            }

            var idsToSelect = numIds
                .Select(id => id.Value);

            var items = await _refuelQueries.GetByIdsAsync(idsToSelect);

            return items.ToList();
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(RefuelViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ArgumentException), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRefuelAsync([FromBody]RefuelItem refuelItem)
        {
            Refuel refuel = _mapper.Map<Refuel>(refuelItem);

            try
            {
                refuel = await _refuelService.CreateAsync(refuel);
            }
            catch (Exception exception)
            {
                BadRequest(exception);
            }
            
            return Ok(_mapper.Map<RefuelViewModel>(refuel));
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RefuelViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateRefuelAsync([FromBody]RefuelItem refuelItem)
        {
            Refuel refuel = await _refuelService.GetByIdAsync(refuelItem.Id);
            if (refuel == null)
                return NotFound(new { Message = $"Refuel with Id {refuelItem.Id} not found" });

            _mapper.Map(refuelItem, refuel);

            await _refuelService.UpdateAsync(refuel);
            
            return Ok(_mapper.Map<RefuelViewModel>(refuel));
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteRefuelAsync(int id)
        {
            Refuel refuel = await _refuelService.GetByIdAsync(id);
            if (refuel == null)
                return NotFound(new { Message = $"Refuel with Id {id} not found" });

            await _refuelService.DeleteAsync(refuel);

            return NoContent();
        }
    }
}