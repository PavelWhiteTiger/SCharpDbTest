using Core.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace MsSqlWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeaponController : Controller
{
    private readonly ApplicationContext _db;

    public WeaponController(ApplicationContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var weapons = _db.Weapons.ToList();
        return Ok(weapons);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int weaponId)
    {
        var weapon = _db.Weapons.FirstOrDefault(x => x.Id == weaponId);
        if (weapon is null)
        {
            return BadRequest("No BRO");
        }

        return Ok(weapon);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] Weapon weapon)
    {
        var res =_db.Weapons.Add(weapon);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] Weapon weapon)
    {
        _db.Weapons.Update(weapon);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromForm] int weaponId)
    {
        var weapon = _db.Weapons.FirstOrDefault(x => x.Id == weaponId);
        if (weapon is null)
        {
            return BadRequest("No BRO");
        }

        _db.Weapons.Remove(weapon);
        await _db.SaveChangesAsync();
        return Ok();
    }
}