using Microsoft.Extensions.Configuration;
using Models.DTOs.v1;
using Models.Finnhub;
using System.Net.Http.Json;

namespace Services;

public interface IStockService
{
    Task<StockQuote?> GetQuoteAsync(string symbol);
}

public class StockService : IStockService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public StockService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["FinnhubApiKey"]!;
    }

    public async Task<StockQuote?> GetQuoteAsync(string symbol)
    {
        var response = await _httpClient.GetFromJsonAsync<FinnhubQuoteResponse>(
            $"quote?symbol={symbol}&token={_apiKey}");

        if (response is null) return null;

        return new StockQuote
        {
            Symbol = symbol.ToUpperInvariant(),
            CurrentPrice = response.CurrentPrice,
            OpenPrice = response.OpenPrice,
            HighPrice = response.HighPrice,
            LowPrice = response.LowPrice,
            PreviousClose = response.PreviousClose,
            Change = response.Change,
            PercentChange = response.PercentChange
        };
    }
}

