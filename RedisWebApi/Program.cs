using RedisWebApi.Interfaces;
using RedisWebApi.Services;
using RedisWebApi.Services.Redis;
using RedisWebApi.Settings;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("Settings\\RedisSettings.json");
builder.Services.Configure<RedisConfiguration>(builder.Configuration.GetSection("redis"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRedisConnection, RedisConnection>();
builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
    ((IRedisConnection)serviceProvider.GetService(typeof(IRedisConnection))!).Connection()!);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class A
{
    public static void main()
    {
        HttpClient client = new HttpClient();
        var allWarriors =  client.GetStringAsync("https://localhost:7158/Warrior/GetAll").Result;
    }
}