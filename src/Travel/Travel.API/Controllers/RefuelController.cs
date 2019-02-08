using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Travel.API.Infrastructure.Services;
using Travel.Infrastructure;

namespace Travel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefuelController : ControllerBase
    {
        public RefuelController(TravelContext context, IMapperService mapper)
        {

        }
    }
}