using MarketPrice.Application.Commands.SyncSupportedAssets;
using MarketPrice.Application.DTOs.Asset;
using MarketPrice.Application.DTOs.Bar;
using MarketPrice.Application.Queries.GetAllAssets;
using MarketPrice.Application.Queries.GetHistoricalPrices;
using MarketPrice.Application.Queries.GetSupportedAssets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketPrice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AssetsController(IMediator mediator) : ControllerBase {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var result = await mediator.Send(new GetAllAssetsQuery());
            return Ok(result);
        }

        [HttpGet("supported")]
        public async Task<ActionResult<List<AssetDto>>> GetSupportedAssets() {
            var result = await mediator.Send(new GetSupportedAssetsQuery());
            return result;
        }

        [HttpGet("historical")]
        public async Task<ActionResult<List<BarDto>>> GetHistoricalPrices(string instrumentId, DateTime startDate, string provider = "oanda") {
            var result = await mediator.Send(new GetHistoricalPricesQuery(instrumentId, startDate, provider));
            return result;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncSupportedAssets() {
            await mediator.Send(new SyncSupportedAssetsCommand());
            return Ok();
        }
    }
}
