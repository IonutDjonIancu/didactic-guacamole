using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<IStockService, StockService>(client =>
{
    client.BaseAddress = new Uri("https://finnhub.io/api/v1/");
});

// once moved to Cosmos DB this becomes AddScoped
builder.Services.AddSingleton<ITradeService, TradeService>();



builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
