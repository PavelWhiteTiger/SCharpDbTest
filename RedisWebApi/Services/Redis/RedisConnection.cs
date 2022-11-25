using Microsoft.Extensions.Options;
using RedisWebApi.Interfaces;
using RedisWebApi.Settings;
using StackExchange.Redis;

namespace RedisWebApi.Services.Redis;

public class RedisConnection : IRedisConnection
{
    /// <summary>
    ///     The _connection.
    /// </summary>
    private readonly ConnectionMultiplexer? _connection;

    public RedisConnection(IOptions<RedisConfiguration> redis)
    {
        _connection = ConnectionMultiplexer.Connect(redis.Value.Host, options => options.AllowAdmin = true);
    }

    public IConnectionMultiplexer Connection() => _connection!;
}