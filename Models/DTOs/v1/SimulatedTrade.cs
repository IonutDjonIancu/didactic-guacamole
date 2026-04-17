namespace Models.DTOs.v1;

public class SimulatedTrade
{
    public Guid Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime PurchasedAt { get; set; }
}

public class SimulatedTradeRequest
{
    public string Symbol { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
