using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinateController : ControllerBase
    {
        CoordinateManager _coordinateManager;

        public CoordinateController(CoordinateManager coordinateManager)
        {
            _coordinateManager = coordinateManager;
        }

        [HttpPost]
        public async Task<List<Location>> PostLocation(Location[] location)
        {
            return _coordinateManager.LocationGenerator(location).Result;
        }

        [HttpPost("Distance")]
        public async Task<List<DistancesFromStabilTags>> PostDistance(Location[] location)
        {
            return _coordinateManager.Distance(location).Result;
        }

        [HttpPost("MessageType")]
        public async Task<List<MessageType44FWModel>> MessageFormat(Location[] location)
        {
            return _coordinateManager.MessageFormat(location).Result;
        }

    }
}
