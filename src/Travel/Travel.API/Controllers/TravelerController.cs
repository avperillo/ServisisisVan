﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel.API.Infrastructure.Queries;
using Travel.API.Infrastructure.Services;
using Travel.API.ViewModel;
using Travel.Domain.AggregatesModel.TravelerAggregate;
using Travel.Infrastructure;

namespace Travel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelerController : ControllerBase
    {
        private readonly ITravelerService _travelerService;
        private readonly ITravelerQueries _travelerQueries;
        private readonly IMapperService _mapper;

        public TravelerController(ITravelerService travelerService,
            ITravelerQueries travelerQueries,
            IMapperService mapper)
        {
            _travelerService = travelerService;
            _travelerQueries = travelerQueries;
            _mapper = mapper;
        }

        // GET api/[controller]/items[?pageSize=3&pageIndex=10]
        // GET api/[controller]/items/1
        [HttpGet]
        [Route("items/{ids?}")]
        [ProducesResponseType(typeof(PaginatedResults<TravelerViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<TravelerViewModel>), (int)HttpStatusCode.OK)]
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

            var totalItems = await _travelerQueries.TravelCountAsync();
            var viewModels = await _travelerQueries.GetPagedAsync(pageSize, pageIndex);

            var results = new PaginatedResults<TravelerViewModel>(pageIndex, pageSize, totalItems, viewModels);

            return Ok(results);
        }

        private async Task<IList<TravelerViewModel>> GetItemsByIdsAsync(string ids)
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));

            if (!numIds.All(nid => nid.Ok))
            {
                return new List<TravelerViewModel>();
            }

            var idsToSelect = numIds
                .Select(id => id.Value);

            return await _travelerQueries.GetByIdsAsync(idsToSelect);
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(TravelerViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTravelerAsync([FromBody]TravelerItem travelerItem)
        {
            Traveler traveler = _mapper.Map<Traveler>(travelerItem);

            try
            {
                await _travelerService.CreateAsync(traveler);
            }
            catch (Exception exception)
            {
                BadRequest(exception);
            }

            TravelerViewModel viewModel = await _travelerQueries.GetByIdAsync(traveler.Id);

            return Ok(viewModel);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(TravelerViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateTravelerAsync([FromBody]TravelerItem travelerItem)
        {
            Traveler traveler = await _travelerService.GetByIdAsync(travelerItem.Id);
            if (traveler == null)
                return NotFound(new { Message = $"Traveler with Id {travelerItem.Id} not found" });

            _mapper.Map(travelerItem, traveler);

            await _travelerService.UpdateAsync(traveler);

            TravelerViewModel viewModel = _mapper.Map<TravelerViewModel>(traveler);

            return Ok(viewModel);
        }

        [Route("deactivate/{id}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TravelerViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeactivateTravelerAsync(int id)
        {
            if (!_travelerQueries.TravelerExists(id))
                return NotFound(new { Message = $"Traveler with Id {id} not found" });

            Traveler traveler = await _travelerService.GetByIdAsync(id);
            try
            {
                await _travelerService.DeactivateAsync(traveler);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }

            TravelerViewModel viewModel = _mapper.Map<TravelerViewModel>(traveler);
            return Ok(viewModel);
        }

        [Route("reactivate/{id}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TravelerViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ReactivateTravelerAsync(int id)
        {
            if (!_travelerQueries.TravelerExists(id))
                return NotFound(new { Message = $"Traveler with Id {id} not found" });

            Traveler traveler = await _travelerService.GetByIdAsync(id);
            try
            {
                await _travelerService.ReactivateAsync(traveler);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }

            TravelerViewModel viewModel = _mapper.Map<TravelerViewModel>(traveler);
            return Ok(viewModel);
        }
    }
}