using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using RedisWebApi.Interfaces;
using RedisWebApi.Models;
using RedisWebApi.Services;
using RedisWebApi.Services.Redis;
using RedisWebApi.Settings;
using StackExchange.Redis;

namespace RedisWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class RedisController : ControllerBase
{
    private readonly IConnectionMultiplexer _multiplexer;
    private readonly RedisConfiguration _redis;
    private readonly IDatabase _db;
    private readonly IServer _server;

    public RedisController(IOptions<RedisConfiguration> redis, IConnectionMultiplexer multiplexer)
    {
        _multiplexer = multiplexer;
        _redis = redis.Value;
        _db = _multiplexer.GetDatabase();
        _server = multiplexer.GetServer(_redis.Host, _redis.Port);
    }

    [HttpPost]
    public IActionResult CreatePerson(Person person)
    {
        person.Id = Guid.NewGuid();
        var key = RedisOperations.GetKey<Person>(person.Id.ToString());
        RedisOperations.AddInHashSet(_db, key!, person);
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdatePerson(Person person)
    {
        var key = RedisOperations.GetKey<Person>(person.Id.ToString());
        var allKeys = _server.Keys();
        if (allKeys.All(x => x != key))
        {
            return NotFound();
        }

        RedisOperations.AddInHashSet(_db, key!, person);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetPerson([FromQuery] string id)
    {
        var key = RedisOperations.GetKey<Person>(id);
        var allKeys = _server.Keys();
        if (allKeys.All(x => x != key))
        {
            return NotFound();
        }

        var person = RedisOperations.GetFromHashSet<Person>(_db, key);
        return Ok(person);
    }

    [HttpGet]
    public IActionResult GetPersons()
    {
        var allKeys = _server
            .Keys()
            .Where(key => key.ToString().StartsWith(nameof(Person)));

        var allPersons = allKeys
            .Select(redisKey => RedisOperations.GetFromHashSet<Person>(_db, redisKey!));

        return Ok(allPersons);
    }

    [HttpDelete]
    public IActionResult DeletePerson([FromQuery] string id)
    {
        var key = RedisOperations.GetKey<Person>(id);
        if (_db.KeyDelete(key))
            return Ok("Успешно удалено");
        return BadRequest("Нет гуида");
    }
}