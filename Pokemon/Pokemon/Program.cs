using PokeApiNet;
using Pokemon.Managers;
using Pokemon.RestClient;
using Pokemon.ShakespeareClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IPokemonManager, PokemonManager>();

var pokeApiClient = new PokeApiClient();
builder.Services.AddSingleton(c => pokeApiClient);
builder.Services.AddSingleton<IShakespeareClient, ShakespeareClient>();
builder.Services.AddSingleton<IHttpRestClient, HttpRestClient>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s=>s.EnableAnnotations());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
