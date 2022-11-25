using StackExchange.Redis;

namespace RedisWebApi.Interfaces;

public interface IRedisConnection
{
    IConnectionMultiplexer? Connection();
}