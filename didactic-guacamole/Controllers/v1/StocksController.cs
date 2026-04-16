using Microsoft.AspNetCore.Mvc;
using Services;

namespace didactic_guacamole.Controllers.v1;

[ApiController]
[Route("api/v1/stocks")]
public class StocksController : ControllerBase
{
    private readonly IStockService _stockService;

    public StocksController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetQuote(string symbol)
    {
        var quote = await _stockService.GetQuoteAsync(symbol);

        if (quote is null)
            return NotFound($"No quote found for symbol: {symbol}");

        return Ok(quote);
    }
}
