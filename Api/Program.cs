using EthereumLibrary.Abstraction;
using EthereumLibrary;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register Services
builder.Services.AddSingleton<IEthereumService>(provider => new EthereumService(
                    "https://holesky.infura.io/v3/ee33907162f3414ca4021cb7f973652c",
                    "0xe7a2277E367ab9945355e2d7Cff79900644D36f3"
                ));
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapFallbackToFile("index.html");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
