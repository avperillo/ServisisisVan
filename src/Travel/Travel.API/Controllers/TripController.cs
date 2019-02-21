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
        private readonly ITripRepository _tripRepository;
        private readonly ITripQueries _tripQuery;
        private readonly ITravelerQueries _travelerQuery;
        private readonly IMapperService _mapper;

        public TripController(ITripRepository tripRepository
                                , ITripQueries tripQuery
                                , ITravelerQueries travelerQuery
                                , IMapperService mapper)
        {
            _tripRepository = tripRepository;
            _tripQuery = tripQuery;
            _travelerQuery = travelerQuery;
            _mapper = mapper;
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(TripViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TripViewModel>> CreateTripAsync(TripItem tripItem)
        {
            if (!_travelerQuery.TravelerExists(tripItem.IdDriver))
                return BadRequest(new TravelerNotExistException($"'{tripItem.IdDriver}' doesn´t exists"));

            Trip trip = new Trip(tripItem.Date, tripItem.IdDriver);
            _mapper.Map(tripItem, trip);

            trip = _tripRepository.Add(trip);
            await _tripRepository.UnitOfWork.SaveChangesAsync();

            TripViewModel tripVM = await _tripQuery.GetTripByIdAsync(trip.Id);

            return Ok(tripVM);
        }



    }
}