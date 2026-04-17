using Microsoft.AspNetCore.Mvc;
using Models.DTOs.v1;
using Services;

namespace didactic_guacamole.Controllers.v1;

[ApiController]
[Route("api/v1/trades")]
public class TradesController : ControllerBase
{
    private readonly ITradeService _tradeService;

    public TradesController(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [HttpPost("buy")]
    public async Task<IActionResult> SimulateBuy([FromBody] SimulatedTradeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Symbol))
            return BadRequest("Symbol is required.");

        if (request.Quantity <= 0)
            return BadRequest("Quantity must be greater than zero.");

        var trade = await _tradeService.SimulateBuy(request);

        if (trade is null)
            return NotFound($"Could not fetch price for symbol: {request.Symbol}");

        return CreatedAtAction(nameof(GetAllTrades), trade);
    }

    [HttpGet]
    public IActionResult GetAllTrades()
    {
        var trades = _tradeService.GetAllTrades();
        return Ok(trades);
    }
}
