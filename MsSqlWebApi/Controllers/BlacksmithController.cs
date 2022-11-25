using Microsoft.AspNetCore.Mvc;
using Core.DbModels;

namespace MsSqlWebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BlacksmithController : Controller
{
    private readonly ApplicationContext _db;

    public BlacksmithController(ApplicationContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var blacksmiths = _db.Blacksmiths.ToList();
        return Ok(blacksmiths);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int blacksmithsId)
    {
        var blacksmith = _db.Blacksmiths.FirstOrDefault(x => x.Id == blacksmithsId);
        if (blacksmith is null)
        {
            return BadRequest("No BRO");
        }

        return Ok(blacksmith);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] Blacksmith blacksmith)
    {
        _db.Blacksmiths.Add(blacksmith);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] Blacksmith blacksmith)
    {
        _db.Blacksmiths.Update(blacksmith);
        await _db.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromForm] int blacksmithId)
    {
        var blacksmith = _db.Blacksmiths.FirstOrDefault(x => x.Id == blacksmithId);
        if (blacksmith is null)
        {
            return BadRequest("No BRO");
        }

        _db.Blacksmiths.Remove(blacksmith);
        await _db.SaveChangesAsync();
        return Ok();
    }
}