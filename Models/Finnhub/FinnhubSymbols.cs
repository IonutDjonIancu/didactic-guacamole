using System.Text.Json.Serialization;

namespace Models.Finnhub;

public class FinnhubSymbolSearchResponse
{
    [JsonPropertyName("count")] public int Count { get; set; }
    [JsonPropertyName("result")] public List<FinnhubSymbolResult> Result { get; set; } = [];
}

public class FinnhubSymbolResult
{
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
    [JsonPropertyName("displaySymbol")] public string DisplaySymbol { get; set; } = string.Empty;
    [JsonPropertyName("symbol")] public string Symbol { get; set; } = string.Empty;
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;
}
