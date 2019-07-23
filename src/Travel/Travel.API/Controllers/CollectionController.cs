using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.API.ViewModel;
using Travel.Domain.AggregatesModel.CollectionAggregate;

namespace Travel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            this.collectionService = collectionService;
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(Collection), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ArgumentException), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRefuelAsync()
        {
            try
            {
                var collection = await collectionService.Create();
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("newentry")]
        [HttpPost]
        [ProducesResponseType(typeof(Collection), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ArgumentException), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> NewEntryAsync([FromBody]EntryViewModel entryViewModel)
        {
            try
            {
                Collection collection = await collectionService.NewEntry(entryViewModel.IdCollection, entryViewModel.Date, entryViewModel.IdTraveler, entryViewModel.Amount);
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("{idCollection}")]
        [HttpGet]
        [ProducesResponseType(typeof(IList<Collection>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ArgumentException), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCollectionByIdAsync(int idCollection)
        {
            try
            {
                Collection collection = await collectionService.GetCollectionAsync(idCollection);
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("close")]
        [HttpPost]
        [ProducesResponseType(typeof(Collection), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ArgumentException), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CloseCollectionAsync(int id)
        {
            try
            {
                await collectionService.CloseCollection(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}