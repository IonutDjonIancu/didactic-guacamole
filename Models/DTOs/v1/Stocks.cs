namespace Models.DTOs.v1;

public class StockQuote
{
    public string Symbol { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal PreviousClose { get; set; }
    public decimal Change { get; set; }
    public decimal PercentChange { get; set; }
}

public class StockSymbolResult
{
    public string Symbol { get; set; } = string.Empty;
    public string DisplaySymbol { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
