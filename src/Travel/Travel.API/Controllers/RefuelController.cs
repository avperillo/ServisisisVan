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
        private readonly IRefuelRepository _refuelRepository;
        private readonly IRefuelQueries _refuelQueries;
        private readonly IMapperService _mapper;

        public RefuelController(IRefuelRepository refuelRepository, IRefuelQueries refuelQueries, IMapperService mapper)
        {
            _refuelRepository = refuelRepository;
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

        [Route("items")]
        [HttpPost]
        [ProducesResponseType(typeof(RefuelViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ArgumentException), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRefuelAsync([FromBody]RefuelItem refuelItem)
        {
            if (refuelItem.Id != 0)
                return BadRequest(new ArgumentException("The 'Refuel.Id' must be different from 0."));

            Refuel refuel = _mapper.Map<Refuel>(refuelItem);

            refuel = _refuelRepository.Add(refuel);
            await _refuelRepository.UnitOfWork.SaveChangesAsync();
            
            return Ok(_mapper.Map<RefuelViewModel>(refuel));
        }

        [Route("items")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RefuelViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateRefuelAsync([FromBody]RefuelItem refuelItem)
        {
            Refuel refuel = await _refuelRepository.GetByIdAsync(refuelItem.Id);
            if (refuel == null)
                return NotFound(new { Message = $"Refuel with Id {refuelItem.Id} not found" });

            _mapper.Map(refuelItem, refuel);

            _refuelRepository.Update(refuel);
            await _refuelRepository.UnitOfWork.SaveChangesAsync();
            
            return Ok(_mapper.Map<RefuelViewModel>(refuel));
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteRefuelAsync(int id)
        {
            Refuel refuel = await _refuelRepository.GetByIdAsync(id);
            if (refuel == null)
                return NotFound(new { Message = $"Refuel with Id {id} not found" });

            _refuelRepository.Remove(refuel);
            await _refuelRepository.UnitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}