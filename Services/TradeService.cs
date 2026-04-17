using Models.DTOs.v1;

namespace Services;

public interface ITradeService
{
    Task<SimulatedTrade?> SimulateBuy(SimulatedTradeRequest request);
    List<SimulatedTrade> GetAllTrades();
}

public class TradeService : ITradeService
{
    private readonly IStockService _stockService;
    private readonly List<SimulatedTrade> _trades = [];

    public TradeService(IStockService stockService)
    {
        _stockService = stockService;
    }

    public async Task<SimulatedTrade?> SimulateBuy(SimulatedTradeRequest request)
    {
        var quote = await _stockService.GetQuote(request.Symbol);

        if (quote is null)
            return null;

        var trade = new SimulatedTrade
        {
            Id = Guid.NewGuid(),
            Symbol = request.Symbol.ToUpperInvariant(),
            Quantity = request.Quantity,
            PurchasePrice = quote.CurrentPrice,
            TotalValue = quote.CurrentPrice * request.Quantity,
            PurchasedAt = DateTime.UtcNow
        };

        _trades.Add(trade);
        return trade;
    }

    public List<SimulatedTrade> GetAllTrades()
    {
        return _trades;
    }
}