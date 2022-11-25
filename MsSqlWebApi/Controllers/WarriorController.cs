using System.ComponentModel.DataAnnotations;
using Core.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MsSqlWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WarriorController : ControllerBase
{
    private readonly ApplicationContext _db;

    public WarriorController(ApplicationContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var warriors = _db.Warriors.ToList();
        return Ok(warriors);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int warriorId)
    {
        var warrior = _db.Warriors.FirstOrDefault(x => x.Id == warriorId);
        if (warrior is null)
        {
            return BadRequest("No BRO");
        }

        return Ok(warrior);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] Warrior warrior)
    {
        _db.Warriors.Add(warrior);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] Warrior warrior)
    {
        _db.Warriors.Update(warrior);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromForm] int warriorId)
    {
        var warrior = _db.Warriors.FirstOrDefault(x => x.Id == warriorId);
        if (warrior is null)
        {
            return BadRequest("No BRO");
        }

        _db.Warriors.Remove(warrior);
        await _db.SaveChangesAsync();
        return Ok();
    }
}