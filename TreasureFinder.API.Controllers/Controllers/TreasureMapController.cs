using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreasureFinder.Service.Contract.Treasures;

namespace TreasureFinder.API.Controllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreasureMapController : ControllerBase
    {
        private readonly ITreasureMapService _treasureMapService;
        public TreasureMapController(ITreasureMapService treasureMapService)
        {
            _treasureMapService = treasureMapService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TreasureMapDto>>> GetAllMaps()
        {
            var maps = await _treasureMapService.GetAllMapsAsync();
            return Ok(maps);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TreasureMapDto>> GetMapById(Guid id)
        {
            var map = await _treasureMapService.GetMapByIdAsync(id);
            if (map == null)
                return NotFound();

            return Ok(map);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMap([FromBody] CreateTreasureMapDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var id = await _treasureMapService.CreateTreasureMapAsync(createDto);

            return CreatedAtAction(nameof(GetMapById), new { id }, id);
        }

        [HttpPost("{id}/recalculate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RecalculateRoute(Guid id)
        {
            var success = await _treasureMapService.RecalculateRouteAsync(id);
            if (!success)
                return NotFound();

            return Ok(new { Message = "Route recalculated successfully" });
        }
    }
}
