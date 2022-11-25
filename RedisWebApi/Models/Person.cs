using System.Text.Json.Serialization;
using RedisWebApi.Interfaces;
using StackExchange.Redis;

namespace RedisWebApi.Models;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string LastName { get; set; }
}