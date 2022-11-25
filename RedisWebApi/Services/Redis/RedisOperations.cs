using StackExchange.Redis;

namespace RedisWebApi.Services.Redis;

public static class RedisOperations
{
    public static RedisKey GetKey<T>(string id)
    {
        return new RedisKey(nameof(T) + id);
    }

    public static void AddInHashSet<T>(IDatabase db, string key, T obj) where T : new()
    {
        var props = obj?.GetType().GetProperties();
        foreach (var property in props!)
        {
            db.HashSet(key, property.Name, property.GetValue(obj)?.ToString());
        }
    }

    public static T GetFromHashSet<T>(IDatabase db, string key) where T : new()
    {
        var obj = new T();
        var props = obj.GetType().GetProperties();
        foreach (var property in props!)
        {
            var value = db.HashGet(key, property.Name);
            if (value.IsInteger)
            {
                property.SetValue(obj, (int)value);
            }
            else
            {
                property.SetValue(obj, value);
            }
        }

        return obj!;
    }
}