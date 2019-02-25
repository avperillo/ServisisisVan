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
                BadRequest(exception);
            }

            TripViewModel tripVM = await _tripQuery.GetTripByIdAsync(trip.Id);

            return Ok(tripVM);
        }



    }
}