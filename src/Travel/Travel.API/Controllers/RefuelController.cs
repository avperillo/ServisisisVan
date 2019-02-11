using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.API.Infrastructure.Services;
using Travel.API.ViewModel;
using Travel.Domain.AggregatesModel.RefuelAggregate;
using Travel.Infrastructure;

namespace Travel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefuelController : ControllerBase
    {
        private readonly TravelContext _context;
        private readonly IMapperService _mapper;

        public RefuelController(TravelContext context, IMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Route("items/{ids?}")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResults<RefuelItem>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<RefuelItem>), (int)HttpStatusCode.OK)]
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

            var totalItems = await _context.Refuels
                .LongCountAsync();

            var items = await _context.Refuels
                .OrderBy(c => c.Date)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            var itemsOnPage = _mapper.Map<List<RefuelItem>>(items.AsEnumerable());

            var results = new PaginatedResults<RefuelItem>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(results);
        }

        private async Task<List<RefuelItem>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<RefuelItem>();
            }

            var idsToSelect = numIds
                .Select(id => id.Value);

            var items = await _context.Refuels.Where(ci => idsToSelect.Contains(ci.Id)).ToListAsync();
            
            return _mapper.Map<List<RefuelItem>>(items);
        }

        [Route("items")]
        [HttpPost]
        [ProducesResponseType(typeof(RefuelItem), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateRefuelAsync([FromBody]RefuelItem refuelItem)
        {
            if (refuelItem.Id != 0)
                throw new ArgumentException("The 'Refuel.Id' must be different from 0.");

            Refuel refuel = _mapper.Map<Refuel>(refuelItem);

            _context.Refuels.Add(refuel);
            await _context.SaveChangesAsync();

            refuelItem = _mapper.Map<RefuelItem>(refuel);

            return Ok(refuelItem);
        }

        [Route("items")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RefuelItem), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateRefuelAsync([FromBody]RefuelItem refuelItem)
        {
            Refuel refuel = await _context.Refuels.SingleOrDefaultAsync(t => t.Id == refuelItem.Id);
            if (refuel == null)
                return NotFound(new { Message = $"Refuel with Id {refuelItem.Id} not found" });

            _mapper.Map(refuelItem, refuel);

            _context.Refuels.Update(refuel);
            await _context.SaveChangesAsync();

            refuelItem = _mapper.Map<RefuelItem>(refuel);

            return Ok(refuelItem);
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteRefuelAsync(int id)
        {
            Refuel refuel = await _context.Refuels.SingleOrDefaultAsync(t => t.Id == id);
            if (refuel == null)
                return NotFound(new { Message = $"Refuel with Id {id} not found" });

            _context.Refuels.Remove(refuel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}