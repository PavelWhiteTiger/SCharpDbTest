using Core.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MsSqlWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BattleController : Controller
{
    private readonly ApplicationContext _db;

    public BattleController(ApplicationContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var battles = _db.Battles.ToList();
        return Ok(battles);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int battleId)
    {
        var battle = _db.Battles.FirstOrDefault(x => x.Id == battleId);
        if (battle is null)
        {
            return BadRequest("No BRO");
        }

        return Ok(battle);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] Battle battle)
    {
        _db.Battles.Add(battle);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] Battle battle)
    {
        _db.Battles.Update(battle);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromForm] int battleId)
    {
        var battle = _db.Battles.FirstOrDefault(x => x.Id == battleId);
        if (battle is null)
        {
            return BadRequest("No BRO");
        }

        _db.Battles.Remove(battle);
        await _db.SaveChangesAsync();
        return Ok();
    }
}