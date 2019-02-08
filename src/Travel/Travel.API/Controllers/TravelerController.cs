using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.API.Model;
using Travel.Domain.AggregatesModel.TravelerAggregate;
using Travel.Infrastructure;

namespace Travel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelerController : ControllerBase
    {

        private readonly TravelContext _travelContext;
        private readonly IMapper _mapper;

        public TravelerController(TravelContext context, IMapper mapper)
        {
            _travelContext = context;
            _mapper = mapper;
        }

        // GET api/[controller]/items[?pageSize=3&pageIndex=10]
        // GET api/[controller]/items/1
        [HttpGet]
        [Route("items/{ids?}")]
        [ProducesResponseType(typeof(IEnumerable<TravelerItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ItemsAsync([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0, string ids = null)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = await GetItemsByIdsAsync(ids);

                if (!items.Any())
                {
                    return BadRequest("ids value invalid. Must be comma-separated list of numbers");
                }

                return Ok(items);
            }

            var totalItems = await _travelContext.Travelers
                .LongCountAsync();

            var itemsOnPage = await _travelContext.Travelers
                .OrderBy(c => c.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            //var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage);

            var model = _mapper.Map<List<TravelerItem>>(itemsOnPage.AsEnumerable());

            return Ok(model);
        }

        private async Task<List<TravelerItem>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<TravelerItem>();
            }

            var idsToSelect = numIds
                .Select(id => id.Value);

            var items = await _travelContext.Travelers.Where(ci => idsToSelect.Contains(ci.Id)).ToListAsync();

            //items = ChangeUriPlaceholder(items);

            return _mapper.Map<List<TravelerItem>>(items);
        }

        [Route("items")]
        [HttpPost]
        [ProducesResponseType(typeof(TravelerItem), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateTravelerAsync([FromBody]TravelerItem travelerItem)
        {
            if (travelerItem.Id != 0)
                throw new ArgumentException("The 'Traveler.Id' must be different from 0.");

            Traveler traveler = _mapper.Map<Traveler>(travelerItem);

            _travelContext.Travelers.Add(traveler);
            await _travelContext.SaveChangesAsync();

            travelerItem = _mapper.Map<TravelerItem>(traveler);

            return Ok(travelerItem);
        }

        [Route("items")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TravelerItem), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateTravelerAsync([FromBody]TravelerItem travelerItem)
        {
            Traveler traveler = await _travelContext.Travelers.SingleOrDefaultAsync(t => t.Id == travelerItem.Id);
            if (traveler == null)
                return NotFound(new { Message = $"Traveler with Id {travelerItem.Id} not found" });

            _mapper.Map(travelerItem, traveler);

            _travelContext.Travelers.Update(traveler);
            await _travelContext.SaveChangesAsync();

            travelerItem = _mapper.Map<TravelerItem>(traveler);

            return Ok(travelerItem);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteTravelerAsync(int id)
        {
            Traveler traveler = await _travelContext.Travelers.SingleOrDefaultAsync(t => t.Id == id);
            if (traveler == null)
                return NotFound(new { Message = $"Traveler with Id {id} not found" });

            _travelContext.Travelers.Remove(traveler);
            await _travelContext.SaveChangesAsync();
            
            return NoContent();
        }
    }
}