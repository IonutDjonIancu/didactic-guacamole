using Microsoft.Extensions.Configuration;
using Models.DTOs.v1;
using Models.Finnhub;
using System.Net.Http.Json;
using System.Text.Json;

namespace Services;

public interface IStockService
{
    Task<StockQuote?> GetQuote(string symbol);
    Task<List<StockSymbolResult>> SearchSymbols(string query);
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

    public async Task<StockQuote?> GetQuote(string symbol)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<FinnhubQuoteResponse>(
                $"quote?symbol={symbol}&token={_apiKey}");

            if (response is null) return null;

            return new StockQuote
            {
                Symbol = symbol.ToUpperInvariant(),
                CurrentPrice = response.CurrentPrice ?? 0,
                OpenPrice = response.OpenPrice ?? 0,
                HighPrice = response.HighPrice ?? 0,
                LowPrice = response.LowPrice ?? 0,
                PreviousClose = response.PreviousClose ?? 0,
                Change = response.Change ?? 0,
                PercentChange = response.PercentChange ?? 0
            };
        }
        catch (JsonException ex)
        {
            // TODO: replace with structured logging via App Insights
            Console.WriteLine($"Deserialization error for symbol {symbol}: {ex.Message}");
            return null;
        }
        catch (HttpRequestException ex)
        {
            // TODO: replace with structured logging via App Insights
            Console.WriteLine($"HTTP error fetching quote for symbol {symbol}: {ex.Message}");
            return null;
        }
    }

    public async Task<List<StockSymbolResult>> SearchSymbols(string query)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<FinnhubSymbolSearchResponse>(
                $"search?q={query}&token={_apiKey}");

            if (response is null) return [];

            return [.. response.Result.Select(r => new StockSymbolResult
            {
                Symbol = r.Symbol,
                DisplaySymbol = r.DisplaySymbol,
                Description = r.Description,
                Type = r.Type
            })];
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserialization error searching symbols for '{query}': {ex.Message}");
            return [];
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP error searching symbols for '{query}': {ex.Message}");
            return [];
        }
    }
}

