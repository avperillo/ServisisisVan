using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.API.Infrastructure.Queries;
using Travel.API.Infrastructure.Services;
using Travel.API.ViewModel;
using Travel.Domain.AggregatesModel.TripAggregate;
using Travel.Domain.Exceptions;

namespace Travel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;
        private readonly ITripQueries _tripQuery;
        private readonly IMapperService _mapper;

        public TripController(ITripService tripService
                                , ITripQueries tripQuery
                                , IMapperService mapper)
        {
            _tripService = tripService;
            _tripQuery = tripQuery;
            _mapper = mapper;
        }

        [Route("items/{ids?}")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResults<TripViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<TripViewModel>), (int)HttpStatusCode.OK)]
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

            var totalItems = await _tripQuery.CountAsync();

            var items = await _tripQuery.GetPagedAsync(pageSize, pageIndex);

            var results = new PaginatedResults<TripViewModel>(pageIndex, pageSize, totalItems, items);

            return Ok(results);
        }

        private async Task<List<TripViewModel>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<TripViewModel>();
            }

            var idsToSelect = numIds
                .Select(id => id.Value);

            var items = await _tripQuery.GetByIdsAsync(idsToSelect);

            return items.ToList();
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(TripViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TripViewModel>> CreateTripAsync(TripItem tripItem)
        {
            Trip trip = new Trip(tripItem.Date, tripItem.IdDriver);
            _mapper.Map(tripItem, trip);

            try
            {
                trip = await _tripService.CreateAsync(trip);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }

            TripViewModel tripVM = await _tripQuery.GetTripByIdAsync(trip.Id);

            return Ok(tripVM);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(TripViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TripViewModel>> DeleteTripAsync(int idTrip)
        {
            try
            {
                await _tripService.DeleteAsync(idTrip);
            }
            catch (Exception exception)
            {
                BadRequest(exception);
            }

            return Ok();
        }

    }
}